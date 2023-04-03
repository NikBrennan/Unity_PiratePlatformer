using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    public GameObject player;
    private CharacterController2D characterController;
    [SerializeField] public SailController sailController;
    public Animator animator;
    public Animator sailAnimator;
	// Start is called before the first frame update
	void Start()
    {
        characterController = player.GetComponent<CharacterController2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.OnShip == true)
        {
            animator.Play("SetSail");
            if (sailController.IsSailing)
            {
                sailAnimator.Play("SailWind");
            } else
            {
				sailAnimator.Play("SailToWind");
			}
            // Transform represents the ship
            player.transform.SetParent(transform);
		}
	}

    public void EndGame()
    {
        // Load the EndGame scene
		SceneManager.LoadScene(0);
	}
}
