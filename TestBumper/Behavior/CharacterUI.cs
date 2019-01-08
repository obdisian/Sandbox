using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wavy
{
    using Interpolation;

    public class CharacterUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _scoreText = null;
        [SerializeField]
        private List<Sprite> _countrySpriteList = new List<Sprite>();

        private CharacterBase _owner = null;
        private Transform _unit = null;
        private Text _nameText = null;
        private Image _countryImage = null;

        private bool _isPlayer = false;

        private float _nameHeight = 0f;



        /// <summary>
        /// init
        /// </summary>
        public void Init(CharacterBase owner)
        {
            _owner = owner;

            _unit = transform.Find("Unit");
            _nameText = _unit.Find("NameText").GetComponent<Text>();
            _countryImage = _unit.Find("Country").GetComponent<Image>();

            if (_owner is Player)
            {
                _isPlayer = true;
            }
        }

        /// <summary>
        /// start
        /// </summary>
        private void Start()
        {
            if (_isPlayer)
            {
                _owner.LevelUpHandler += (nowLevel, addLevel) =>
                {
                    var obj = Instantiate(_scoreText, _unit);
                    var text = obj.GetComponent<Text>();
                    text.text = $"+{addLevel}";

                    float top = 100f;
                    text.Easing_Color(Easing.Type.Cubic_Out, new Color(1f, 1f, 1f, 0f), Color.white, 0.25f);
                    obj.GetComponent<RectTransform>().Easing_LocalPosition(Easing.Type.Cubic_Out, Vector3.zero, Vector3.up * top, 0.25f).DestroyHandler += () =>
                    {
                        //text.Easing_Color(Easing.Type.Quintic_InOut, Color.white, new Color(1f, 1f, 1f, 0f), 0.5f);
                        //obj.GetComponent<RectTransform>().Easing_LocalPosition(Easing.Type.Quintic_InOut, Vector3.up * top, Vector3.up * top*2f, 0.5f);
                    };
                    obj.transform.Easing_LocalScale(Easing.Type.Cubic_Out, Vector3.one, Vector3.one * 1.5f, 0.5f).DestroyHandler += () =>
                    {
                        obj.transform.Easing_LocalScale(Easing.Type.Quintic_InOut, Vector3.one * 1.5f, Vector3.zero, 0.5f).DestroyHandler += () =>
                        {
                            Destroy(obj);
                        };
                    };
                };
                _nameText.alignment = TextAnchor.MiddleCenter;
                _nameText.rectTransform.localPosition = new Vector3(0f, _nameText.rectTransform.localPosition.y, 0f);
                _nameText.text = $"Player"; // セーブデータに差し替える
                _countryImage.gameObject.SetActive(false);
                _nameText.gameObject.SetActive(false);
            }
            else
            {
                // ランダムで名前を取得する
                var list = DataManager.Instance.CountryNameDataList;
                int countryIndex = Random.Range(0, list.Count);
                string aiName = list[countryIndex].GetRandomName();
                _nameText.text = aiName == string.Empty ? $"BOT-{(int)Random.Range(0, 1000f)}" : aiName;
                _countryImage.sprite = _countrySpriteList[countryIndex];
            }

            _nameHeight = _owner.transform.localScale.x * 2f;
            _unit.position = _owner.transform.position + Vector3.up * _nameHeight;
            _unit.localScale = _owner.transform.localScale;
            _unit.rotation = FindObjectOfType<Camera>().transform.rotation;
        }

        /// <summary>
        /// late update
        /// </summary>
        private void LateUpdate()
        {
            if (!_owner)
            {
                Destroy(gameObject);
                return;
            }

            float t = 0.075f;
            _nameHeight = Mathf.Lerp(_nameHeight, _owner.transform.localScale.x * 2f, t);
            _unit.position = _owner.transform.position + Vector3.up * _nameHeight;
            _unit.localScale = Vector3.Lerp(_unit.localScale, Vector3.one * _owner.transform.localScale.x * Mathf.Pow(0.925f, _owner.Level - 1), t);
            _unit.rotation = FindObjectOfType<Camera>().transform.rotation;
        }
    }
}
