# Interfaces Inteligentes P04
## Ejercicio 1

#### El **cubo** tiene un script para que el usuario lo pueda mover llamado `Move.cs`. Utiliza los mismos conceptos que la práctiva anterior

***Nota:*** El cubo es un objeto físico (contiene `Rigidbody` y `Collider`)

```cs
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
```

```cs
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical);
        
        if (direction.magnitude > 0)
        {
            direction.Normalize();

            Quaternion rotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, rotation, rotationSpeed * Time.fixedDeltaTime));

            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
    }
```

#### El **cilindro** contiene el script `CyllinderCollider.cs`. El cual utiliza el `patrón obervador`
* El **Patrón Obervador funciona de la siguiente manera**:
  
  1. Se define el delegado y evento
     
    * El **delegado** actua como la estructura para los métodos que reaccionarán ante ciertos eventos. En este caso, el delegado `CubeCollidedWithSphere` representará los métodos cuando el cubo colisione con el cilindro.
    * El **evento estático de tipo de delegado** es declarado para que cualquier clase pueda suscribirse a este evento
      
  2. Cuando ocurre una colisión (en este caso entre el cubo y el cilindro), se invoca al evento, notificando a todos los suscriptores que ha ocurrido la colisión
  3. Otras clases se suscriben al evento con `+=` en `OnEnable()` y desuscriben con `-=` en `OnDisable()`. Así las esferas escuchan el evento y reaccionan cuando ocurra
  4. Cuando el evento se dispara, los suscriptores (en este caso las esferas) reaccionan ejecutándo un método

```cs
public class CollisionNotifier : MonoBehaviour
{
    public delegate void CubeCollidedWithSphere();
    public static event CubeCollidedWithSphere OnCubeCollisionWithSphere;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            OnCubeCollisionWithSphere?.Invoke();
        }
    } 
}
```
#### Las esferas de tipo 1 y tipo 2 tienen sus controladores `Spherecontroller1.cs` y `SphereController2.cs` respectivamente

***Nota:*** Ambos scripts son iguales exeptuando el nombre de la función que llama a `MoveTowards` y el nombre del objeto al que se dirigen

```cs
    public Transform target;
    private GoTowards goTowards;
```
```cs
    void Start()
    {
        goTowards = GetComponent<GoTowards>();
    }
```

Aquí simplemente creamos los atribudos para establecer el objetivo (`target`) y el `GoTowards`. En el Start() se recupera ese componente, el cual es otro script

```cs
    void OnEnable()
    {
        CyllinderCollider.OnCubeCollisionWithSphere += GoToSphere;
    }

    void OnDisable()
    {
        CyllinderCollider.OnCubeCollisionWithSphere -= GoToSphere;
    }
```

Esto es para el `el paso 3` del **patrón obervador**, la suscripción

```cs
    void GoToSphere()
    {
        if (goTowards != null && target != null)
        {
            goTowards.SetTarget(target);
        }
    }
```

Se etsablece el `target` para que las esferas sepan hacian dónde moverse

#### Todas las esferas contienen el script `MoveTowards`, que simplemente aplica la lógica de movimeinto

```cs
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = null;
    }
```
El target se declara `null` al principio para que las esferas no se muevan hasta que el cubo collisione al cilindro y desde el controlador de llame a `SetTarget`

```cs
    void FixedUpdate()
    {
        if (target != null && rb != null)
        {
            Vector3 direction = target.position - transform.position;
            rb.MovePosition(transform.position + direction.normalized * speed * Time.fixedDeltaTime);
        }
    }
```
Mueve el objeto hacia un objetivo calculando la dirección y actualizando su posición en cada frame fijo usando físicas

```cs
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public Transform GetTarget()
    {
        return target;
    }
```
También incluye un `setter` y un `getter`

![ej 1](docs/p04_001.gif)

## Ejercicio 2
Ahora tenemos una escena con `arañas` y `huevos`
- Las `arañas verdes` son de **tipo 1** (equivalente a esferas de tipo 1)
- Las `arañas rojas` son de **tipo 2** (equivalente a esferas de tipo 2)
- Los `huevos verdes` son de **tipo 1** (a la que se acercan equivale al cilindro)
- Los `arañas rojos` son de **tipo 2**

![ej 2](p04_002.gif)

## Ejercicio 3
## Ejercicio 4
## Ejercicio 5
![ej 5](p04_005.gif)
## Ejercicio 6
![ej 6](p04_006.gif)
## Ejercicio 7
![ej 7](p04_007.gif)
## Ejercicio 8
## Ejercicio 9
El ccubo ya era un objeto físico
