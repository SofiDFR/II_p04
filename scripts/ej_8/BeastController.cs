using UnityEngine;

public class BichoController : MonoBehaviour
{
    private GoTowards goTowards;        // Referencia al componente GoTowards
    public float speedIncrease = 1f;    // Velocidad que se añadirá cuando se recoja una moneda

    // Nos suscribimos al evento cuando el objeto está habilitado
    void OnEnable()
    {
        Coin.OnCoinCollected += OnCoinCollected;
    }

    // Nos desuscribimos del evento cuando el objeto está deshabilitado
    void OnDisable()
    {
        Coin.OnCoinCollected -= OnCoinCollected;
    }

    void Start()
    {
        // Obtener el componente GoTowards
        goTowards = GetComponent<GoTowards>();
    }

    // Este método se llama cuando se recoge una moneda
    void OnCoinCollected()
    {
        // Aumentamos la velocidad del bicho
        goTowards.IncreaseSpeed(speedIncrease);
    }
}
