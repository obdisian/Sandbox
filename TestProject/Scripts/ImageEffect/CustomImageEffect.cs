using UnityEngine;

//	[always included shaders]にシェーダーを追加する
[RequireComponent(typeof(Camera))]
public abstract class CustomImageEffect : MonoBehaviour
{
	private Material mMaterial;

	public abstract string ShaderName { get; }
	protected Material Material { get { return mMaterial; } }


	protected virtual void Awake ()
	{
		Shader shader = Shader.Find (ShaderName);
		mMaterial = new Material (shader);
	}

	protected virtual void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		UpdateMaterial ();
		Graphics.Blit (source, destination, mMaterial);
	}

	protected abstract void UpdateMaterial ();
}