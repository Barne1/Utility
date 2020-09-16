using UnityEngine;

public class CameraShake : MonoBehaviour {

    [SerializeField]float initalStrength;
    float strength;
    [SerializeField] float speed;
    [SerializeField, Range(0f, 1f)] float dampingPercent;
    

    private Vector2 nextPosition;
    private Vector2 defaultPosition;

    private void Awake() {
        defaultPosition = transform.position;
        nextPosition = defaultPosition;
        Enemy.OnDeath.AddListener(ShakeCamera);
    }

    private void Update() {
        if (strength > 0.01f) {
            if (nextPosition.WithinDistance(transform.position, 0.001f)) {
                strength *= dampingPercent;
                nextPosition = Random.insideUnitCircle.normalized * strength;
                Debug.Log(nextPosition);
            }
            else {
                Vector2 nextTransFormPos = Vector2.Lerp(transform.position, nextPosition, speed * Time.deltaTime);
                Vector3 nextTransFormPosV3 = new Vector3(nextTransFormPos.x, nextTransFormPos.y, transform.position.z);
                transform.position = nextTransFormPosV3;

            }
        }
    }

    public void ShakeCamera(Enemy enemy) {
        this.strength = initalStrength;
    }
}
