using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ball : MonoBehaviour {

    public float ballSpeed = 15.0f;    
    public static int playerScore = 0;
    public static int enemyScore = 0;
	public Text playerText;
	public Text enemyText;
	public AudioSource[] source;
	//public AudioSource boundsAudio;
	//public AudioSource goalAudio;
	//public AudioSource racketAudio;


    Rigidbody2D rb;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
		rb.velocity = Vector2.right * ballSpeed;
		updateScore (playerText, 0);
		updateScore (enemyText, 0);
		source = GetComponents<AudioSource>();
	}


	void OnCollisionEnter2D(Collision2D col) {
		// Note: 'col' holds the collision information. If the
		// Ball collided with a racket, then:
		//   col.gameObject is the racket
		//   col.transform.position is the racket's position
		//   col.collider is the racket's collider

		// play a sound when ball hits upper or lower walls
		if (col.gameObject.name == "topBounds" || col.gameObject.name == "bottomBounds") {
			source [2].Play ();
		}
			

		// Hit the left Racket?
		if (col.gameObject.name == "Player") {
			source [3].Play ();
			//Debug.Log ("Player");
			hitBall(1, col);
		}

		// Hit the right Racket?
		if (col.gameObject.name == "Enemy") {
			source [3].Play ();
			//Debug.Log ("Enemy");
			hitBall(-1, col);
		}			

		// keep score
		if (col.gameObject.name == "leftGoal") {
			source [1].Play ();
			updateScore (enemyText, ++enemyScore);
		}
		if (col.gameObject.name == "rightGoal") {			
			source [0].Play ();
			updateScore (playerText, ++playerScore);
		}

		// MUST HAVE A CHECK SO THIS DO NOT CONTINUE TO SPEED UP WITHOUT INCREASING SCORE
//		if (playerScore > 0 || enemyScore > 0) {
//			if (playerScore % 10 == 0 || enemyScore % 10 == 0) {
//				ballSpeed *= 1.25f;
//			}
//		}
	}

	void hitBall(int value, Collision2D col) {
		// Calculate hit Factor
		float y = hitFactor(transform.position,
			col.transform.position,
			col.collider.bounds.size.y);

		// Calculate direction, make length=1 via .normalized
		Vector2 dir = new Vector2(1*value, y).normalized;

		// Set Velocity with dir * speed
		rb.velocity = dir * ballSpeed;
		rb.transform.Rotate (0, 180, 0);
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (col.gameObject.name == "leftGoal") {
			//updateScore (enemyText, ++enemyScore);
		}
		if (col.gameObject.name == "rightGoal") {			
			//updateScore (playerText, ++playerScore);
		}
		//rb.transform.Translate (new Vector3 ());
	}

	void updateScore(Text text, int score) {
		text.text = "Score: " + score.ToString ();
	}

	float hitFactor(Vector2 ballPos, Vector2 racketPos,
		float racketHeight) {
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return (ballPos.y - racketPos.y) / racketHeight;
	}
}
