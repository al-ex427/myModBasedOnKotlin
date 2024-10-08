using UnityEngine;

public class AnimateOnEnable : MonoBehaviour
{
	public Animator animator;

	public new string name;

	private void OnEnable()
	{
		animator.Play(name, -1, 0f);
	}
}
