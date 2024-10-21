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

![ej 2](docs/p04_002.gif)

## Ejercicio 3
* **Cubo** colisiona con **Araña Tipo 2 (Roja)** -> **Araña Tipo 1 (Verde)** se dirige hacia el **Cofre del Tesoro**

Para detectar que el cubo ha colisionado contra la araña del tipo dos, se urará el patrón obervador. E lscript del notificador irá en la araña de tipo 2.

```cs
public enum SpiderType
{
    Type1,
    Type2
}
```
Aquí definimos los tipos de arañas que pueden haber y se asigna en el inspector.

```cs
public class CollisionNotifier : MonoBehaviour
{
    public delegate void CubeCollidedWithSpider(SpiderType spiderType);
    public static event CubeCollidedWithSpider OnCubeCollisionWithSpider;
    public SpiderType spiderType;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            OnCubeCollisionWithSpider?.Invoke(spiderType);
        }
    } 
}
```
El notificador es igual al anterior, simplemente se añade como parámetro el tipo de la araña para que el controlador de las `arañas de tipo 1` sepan hacia dónde dirigirse.

El controlador de las arañas de tipo 1 también se suscriben y desuscriben al evento como se indicó antes. Veamos las funciones propias de este controlador:

```cs
CollisionNotifier.OnCubeCollisionWithSpider += HandleCubeCollisionWithSpider;
```

```cs
    void HandleCubeCollisionWithSpider(SpiderType spiderType)
    {
        if (spiderType == SpiderType.Type1)
        {
            MoveTowardsNearestType2Egg();
        }
        else if (spiderType == SpiderType.Type2)
        {
            MoveTowardsSelectedObject();
        }
    }
```

Dependiendo del tipo de araña con el que ha colisionado el cubo, llamará a una función u otra.
```cs
    void MoveTowardsSelectedObject()
    {
        if (goTowards != null && selectedObject != null)
        {
            goTowards.SetTarget(selectedObject);
        }
    }
```

* **Cubo** colisiona con **Araña Tipo 1** -> **Araña Tipo 1** se dirige hacia el **Huevo Tipo 2** más cercano
* 
```cs
    void MoveTowardsNearestType2Egg()
    {
        Transform nearestEgg = FindNearestEggOfType2();
        if (goTowards != null && nearestEgg != null)
        {
            goTowards.SetTarget(nearestEgg);
        }
    }
```
* **Araña Tipo 1** cambia de color al colisionar con **Huevo Tipo 2**
  
```cs
    void OnCollisionEnter(Collision collision)
    {
     // . . .    
        if (collision.gameObject.tag == "EggType2" && spiderRenderer != null)
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value, 1.0f);
            spiderRenderer.material.color = randomColor;

            spiderRenderer.enabled = false;
            spiderRenderer.enabled = true;
        }
    }
```
AL detectar una colisión con un huevo de tipo 2, cambia el color del objeto a uno aleatorio y luego desactiva y reactiva el renderizador para forzar la actualización visual

![ej 3](docs/p04_003.gif)

## Ejercicio 4
Se han añadido dos `árboles` a la escena. El árbol de la derecha será el de referencia y el de la izquierda al que se teletransportarán las arañas de tipo 1 y orientarán las arañas de tipo 2.
* **Detector de proximidad**
```cs
    public delegate void OnProximityEvent();
    public static event OnProximityEvent OnProximity;
```
```cs
    void Update()
    {
        float distance = Vector3.Distance(transform.position, referenceObj.position);

        if (distance < distanceThreshold && !hasTeleported)
        {
            if (OnProximity != null)
            {
                OnProximity?.Invoke();
                hasTeleported = true;
            }
        }
        if (distance >= distanceThreshold)
        {
            ResetTeleport();
        }
    }
```
Esta vez se llama en el update, comprobando en cada frame la distancia hasta el objetivo.

La variable `hasTeleported` simplemente indica que si se ha entrado en esa proximidad, que las arañas solo se teletransporten 1 vez. Solo vuelven a teletransportarse si se sale y vuelve a entrar

* **Arañas de Tipo 1**

En el controlador que ya tenían, ahora se suscriben a:
```cs
ProximityDetector.OnProximity += TeleportToObj;
```

Y se cambia su posición al objeto seleccionado
```cs
    void TeleportToObj()
    {
        if (teleportObj != null)
        {
            rb.MovePosition(teleportObj.position);
        }
    }
```
![ej 4](docs/p04_004.gif)
## Ejercicio 5
![ej 5](docs/p04_005.gif)
![points](docs/points.PNG)
## Ejercicio 6
![ej 6](docs/p04_006.gif)
## Ejercicio 7
![ej 7](docs/p04_007.gif)
## Ejercicio 8
## Ejercicio 9
El cubo ya era un objeto físico
