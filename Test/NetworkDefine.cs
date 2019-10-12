using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
    public static partial class NetworkDefine
    {
        public class SyncPacket
        {
            public byte[] Datas;
        }

        public abstract class PacketSuperBase
        {
            #region 定義
            public const int Bit = 8;
            public class Unit
            {
                public byte[] Value;

                #region コンストラクタ

                #region 1byte
                public Unit(ref byte value, byte[] recieveData)
                {
                    if (recieveData != null)
                    {
                        value = recieveData[0];
                    }
                    else
                    {
                        Value = new byte[1];
                        Value[0] = value;
                    }
                }
                public Unit(ref sbyte value, byte[] recieveData)
                {
                    if (recieveData != null)
                    {
                        value = (sbyte)recieveData[0];
                    }
                    else
                    {
                        Value = new byte[1];
                        Value[0] = (byte)value;
                    }
                }
                #endregion

                #region 2byte
                public Unit(ref ushort value, byte[] recieveData)
                {
                    if (recieveData != null)
                    {
                        value = System.BitConverter.ToUInt16(recieveData, 0);
                    }
                    else
                    {
                        Value = System.BitConverter.GetBytes(value);
                    }
                }
                public Unit(ref short value, byte[] recieveData)
                {
                    if (recieveData != null)
                    {
                        value = System.BitConverter.ToInt16(recieveData, 0);
                    }
                    else
                    {
                        Value = System.BitConverter.GetBytes(value);
                    }
                }
                #endregion

                #region 4byte
                public Unit(ref uint value, byte[] recieveData)
                {
                    if (recieveData != null)
                    {
                        value = System.BitConverter.ToUInt32(recieveData, 0);
                    }
                    else
                    {
                        Value = System.BitConverter.GetBytes(value);
                    }
                }
                public Unit(ref int value, byte[] recieveData)
                {
                    if (recieveData != null)
                    {
                        value = System.BitConverter.ToInt32(recieveData, 0);
                    }
                    else
                    {
                        Value = System.BitConverter.GetBytes(value);
                    }
                }
                public Unit(ref float value, byte[] recieveData)
                {
                    if (recieveData != null)
                    {
                        value = System.BitConverter.ToSingle(recieveData, 0);
                    }
                    else
                    {
                        Value = System.BitConverter.GetBytes(value);
                    }
                }
                #endregion

                #region 8byte
                #endregion

                #endregion
            }
            #endregion

            #region 派生先定義
            protected abstract int FlagSize { get; }
            public abstract Unit[] getUnits(SyncPacket packet);
            public abstract void send();
            public abstract void recieve(SyncPacket packet);
            #endregion

            #region フィールド プロパティ
            protected int MemberMaxSize => FlagSize * Bit;
            protected byte[] _syncFlags = null;
            protected byte getSyncBit(int memberIndex) => (byte)(1 << (memberIndex % Bit));
            #endregion

        }
        public abstract class PacketBase<T> : PacketSuperBase where T : PacketSuperBase, new()
        {
            private T _defaultPacket = null;
            public T DefaultPacket
            {
                get => _defaultPacket ?? (_defaultPacket = new T());
                //set => _defaultPacket = value;
            }
            protected virtual bool IsOverwriteDefault => false;


            // 消す予定。仮でsendしたらここに値を突っ込む
            [System.Obsolete]
            public SyncPacket SendPacket = null;

#if UNITY_EDITOR
            public string BitString = "";
#endif

            #region 送信
            private List<byte> _sendByteList = new List<byte>();

            /// <summary>
            /// 送信
            /// </summary>
            public sealed override void send()
            {
                _sendByteList.Clear();
                _syncFlags = new byte[FlagSize];

                var units = getUnits(null);
                var defaultUnits = DefaultPacket.getUnits(null);

                int count = Mathf.Min(units.Length, MemberMaxSize);
                for (int i = 0; i < count; i++)
                {
                    int syncFlagsIndex = i / Bit;

                    var unit = units[i];
                    var defaultUnit = defaultUnits[i];

                    // データが一致するかチェック
                    bool isMatch = true;
                    for (int j = 0; j < unit.Value.Length; j++)
                    {
                        if (unit.Value[j] != defaultUnit.Value[j])
                        {
                            isMatch = false;
                            break;
                        }
                    }
                    // 一致しない場合はパケットに詰める
                    if (!isMatch)
                    {
                        _syncFlags[syncFlagsIndex] |= getSyncBit(i);
                        _sendByteList.AddRange(unit.Value);
                    }
                }

                bool isSend = _sendByteList.Count > 0;
                if (isSend)
                {
                    var packet = new SyncPacket();

                    packet.Datas = new byte[FlagSize + _sendByteList.Count];

                    // フラグの設定
                    for (int i = 0; i < FlagSize; i++)
                    {
                        packet.Datas[i] = _syncFlags[i];
                    }

                    // 値の設定
                    for (int i = FlagSize; i < packet.Datas.Length; i++)
                    {
                        packet.Datas[i] = _sendByteList[i - FlagSize];
                    }

                    SendPacket = packet;

#if UNITY_EDITOR
                    BitString = ""; foreach (var flag in _syncFlags) { for (int i = 0; i < MemberMaxSize; i++) { BitString += (flag & getSyncBit(i)) != 0 ? "1" : "0"; } }
#endif
                    Debug.Log($"send = {packet.Datas.Length}byte");


                    // 送信に成功したらデフォルトの値を送信する
                    if (IsOverwriteDefault)
                    {
                        DefaultPacket.recieve(SendPacket);
                    }
                }
                else
                {
                    SendPacket = null;
                }
            }
            #endregion

            #region 受信
            private int _memberIndex = 0;
            private int _packetDataIndex = 0;

            /// <summary>
            /// パケットの受信
            /// </summary>
            public sealed override void recieve(SyncPacket packet)
            {
                if (packet == null) { return; }

                // フラグの設定
                _syncFlags = new byte[FlagSize];
                for (int i = 0; i < FlagSize; i++)
                {
                    _syncFlags[i] = packet.Datas[i];
                }
                _memberIndex = -1;
                _packetDataIndex = 0;

#if UNITY_EDITOR
                BitString = ""; foreach (var flag in _syncFlags) { for (int i = 0; i < MemberMaxSize; i++) { BitString += (flag & getSyncBit(i)) != 0 ? "1" : "0"; } }
#endif

                // データの設定
                getUnits(packet);
            }
            #endregion

            #region 公開メソッド
            public byte[] getBytes(SyncPacket packet)
            {
                if (packet == null) { return null; }

                _memberIndex++;
                int syncFlagsIndex = _memberIndex / Bit;

                // データに変更がないとき
                if ((_syncFlags[syncFlagsIndex] & getSyncBit(_memberIndex)) == 0)
                {
                    return null;
                }

                // データに変更があるので新規で設定
                var defaultUnits = DefaultPacket.getUnits(null);
                byte[] bytes = new byte[defaultUnits[_memberIndex].Value.Length];
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = packet.Datas[FlagSize + _packetDataIndex];
                    _packetDataIndex++;
                }
                return bytes;
            }
            #endregion
        }


        #region サイズごとの定義
        public abstract class PacketBase_8<T> : PacketBase<T> where T : PacketBase<T>, new() { protected sealed override int FlagSize => sizeof(byte); }
        public abstract class PacketBase_16<T> : PacketBase<T> where T : PacketBase<T>, new() { protected sealed override int FlagSize => sizeof(byte) * 2; }
        public abstract class PacketBase_24<T> : PacketBase<T> where T : PacketBase<T>, new() { protected sealed override int FlagSize => sizeof(byte) * 3; }
        public abstract class PacketBase_32<T> : PacketBase<T> where T : PacketBase<T>, new() { protected sealed override int FlagSize => sizeof(byte) * 4; }
        #endregion


        #region テスト
        /// <summary>
        /// テストパケット
        /// </summary>
        [System.Serializable]
        public class SyncAction : PacketBase_8<SyncAction>
        {
            protected override bool IsOverwriteDefault => true;

            /// <summary>
            /// マクロで自動生成したい
            /// </summary>
            /// <returns>The units.</returns>
            /// <param name="packet">Packet.</param>
            public override Unit[] getUnits(SyncPacket packet)
            {
                return new Unit[]
                {
                    new Unit(ref UniqueId, getBytes(packet)),
                    new Unit(ref ActionId, getBytes(packet)),
                    new Unit(ref PosX, getBytes(packet)),
                    new Unit(ref PosY, getBytes(packet)),
                    new Unit(ref PosZ, getBytes(packet)),
                };
            }

            public int UniqueId = 0;
            public int ActionId = 0;
            public float PosX = 0;
            public float PosY = 0;
            public float PosZ = 0;
        }


        /// <summary>
        /// テストオブジェクト
        /// </summary>
        [System.Serializable]
        public class TestPacketObject
        {
            public SyncAction SyncActionInfo = null;
            public SyncAction RecieveInfo = null;

            private bool _isFirst = true;
            private float _timer = 0;
            private float _maxTime = 0;

            public void update()
            {
                if (_isFirst)
                {
                    _isFirst = false;
                    SyncActionInfo.UniqueId = Random.Range(0, byte.MaxValue);
                }

                _timer += Time.deltaTime;
                if (_timer > _maxTime)
                {
                    _timer = 0f;
                    _maxTime = Random.Range(0f, 3f);
                    _maxTime = 1f;
                    SyncActionInfo.ActionId = Random.Range(0, byte.MaxValue);

                    if (Random.Range(0, 2) == 0) { SyncActionInfo.PosX = Random.Range(float.MinValue, float.MaxValue); }
                    if (Random.Range(0, 2) == 0) { SyncActionInfo.PosY = Random.Range(float.MinValue, float.MaxValue); }
                    if (Random.Range(0, 2) == 0) { SyncActionInfo.PosZ = Random.Range(float.MinValue, float.MaxValue); }
                }

                // 送信
                SyncActionInfo.send();

                if (SyncActionInfo.SendPacket != null)
                {
                    //RecieveInfo = new SyncAction();
                    RecieveInfo.recieve(SyncActionInfo.SendPacket);
                }
            }
        }
        #endregion
    }
}
