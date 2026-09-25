<div align="center">

# 🔐 Lockers Inteligentes

**Sistema de gestión de lockers para la recepción de paquetes en edificios residenciales**

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-latest-239120?logo=csharp&logoColor=white)
![WinForms](https://img.shields.io/badge/UI-Windows%20Forms-0078D6?logo=windows&logoColor=white)
![SQL Server](https://img.shields.io/badge/DB-SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![Estado](https://img.shields.io/badge/estado-en%20desarrollo-yellow)

<img src="docs/locker-animado.svg" alt="Animación: un repartidor ingresa el PIN, el locker se abre, deja el paquete y queda Ocupado" width="640"/>

</div>

---

## 📦 ¿De qué se trata?

**Lockers Inteligentes** resuelve un problema muy cotidiano: los paquetes que llegan a un edificio cuando el residente no está. En lugar de dejarlos en portería, cada edificio cuenta con un banco de lockers de distintos tamaños.

El flujo es simple:

1. **Reserva** – un operador registra que llega un paquete para un residente. El sistema busca automáticamente un locker **libre** del **tamaño adecuado** (o mayor) **dentro del edificio del residente**.
2. **PIN de un solo uso** – se genera un PIN de 6 dígitos criptográficamente seguro, válido por **48 horas**, y se envía por email al residente.
3. **Entrega** – el repartidor usa el PIN para abrir el locker y deja el paquete. El locker pasa a **Ocupado**.
4. **Retiro** – el residente tiene **7 días** para retirarlo. Pasado ese plazo el locker se marca como **Vencido**.

Todo se opera desde una aplicación de escritorio con control de acceso por roles (**Administrador** y **Operador**) y soporte multi-idioma (español / inglés).

---

## ✨ Funcionalidades

| Módulo | Funcionalidad | Estado |
|---|---|:---:|
| **Seguridad** | Login con contraseña hasheada + salt, cierre de sesión sin reiniciar la app | ✅ |
| | Registro y ABM de usuarios, roles Administrador / Operador | ✅ |
| **Gestión** | ABM de Edificios, Lockers, Residentes y Repartidores | ✅ |
| **Operación** | Panel visual del estado de los lockers por edificio y sector | ✅ |
| | Reservar locker (CU.Op.001) + generación de PIN + notificación por email | ✅ |
| | Registrar entrega (CU.Op.002) | 🚧 |
| | Registrar retiro (CU.Op.003) | 🚧 |
| **Información** | Reportes y Bitácora de operaciones | 🚧 |
| **Idioma** | Traducciones almacenadas en base de datos (es-AR / en-US) | 🚧 |

---

## 🔄 Ciclo de vida de un locker

Las transiciones de estado **no se validan en los servicios, sino en las propias entidades**. Si alguien intenta reservar un locker que no está libre, es `Locker.Reservar()` quien lanza la excepción.

```mermaid
stateDiagram-v2
    [*] --> Libre
    Libre --> Reservado : Reservar()
    Reservado --> Ocupado : Ocupar()<br/>repartidor usa el PIN
    Ocupado --> Libre : Liberar()<br/>residente retira
    Reservado --> Libre : Liberar()<br/>reserva cancelada/expirada
    Libre --> FueraDeServicio : PonerFueraDeServicio()
    Reservado --> FueraDeServicio : PonerFueraDeServicio()
    note right of Ocupado
        No se puede dar de baja
        un locker con un paquete adentro
    end note
```

La **Orden de Entrega** tiene su propio ciclo, también protegido por la entidad:

```mermaid
stateDiagram-v2
    direction LR
    [*] --> Reservada
    Reservada --> Entregada : MarcarEntregada()
    Entregada --> Retirada : MarcarRetirada()
    Retirada --> [*]
```

---

## 🏛️ Arquitectura

El sistema está organizado en **capas**, cada una en su propio proyecto dentro de la solución. Las dependencias van siempre "hacia abajo": la UI nunca habla directamente con la base de datos.

```mermaid
flowchart TD
    UI["🖥️ UI<br/><small>Windows Forms</small>"]
    BLL["⚙️ BLL<br/><small>Servicios y reglas de negocio</small>"]
    DAL["🗄️ DAL<br/><small>Repositorios + ADO.NET</small>"]
    ARQ["🧰 ArqBase<br/><small>Encriptación, Email, Errores</small>"]
    DOM["🧩 Dominio<br/><small>Entidades y Enums</small>"]
    DB[("SQL Server")]

    UI --> BLL
    BLL --> DAL
    BLL --> ARQ
    DAL --> ARQ
    DAL --> DB
    UI -.-> DOM
    BLL -.-> DOM
    DAL -.-> DOM
    ARQ -.-> DOM
```

| Capa | Responsabilidad |
|---|---|
| **Dominio** | Entidades del negocio (`Locker`, `OrdenDeEntrega`, `CodigoAcceso`, `Residente`, …) con sus reglas de transición. No depende de nada. |
| **ArqBase** | Servicios transversales que no tocan la base: hashing (`EncriptadorService`), envío de mails (`EmailService`) y registro de errores. |
| **DAL** | Acceso a datos con `Microsoft.Data.SqlClient`. Un repositorio por entidad + scripts SQL. |
| **BLL** | Orquesta los casos de uso: login, sesión, reservas, ABM, estado de lockers. |
| **UI** | Formularios WinForms agrupados por módulo (Seguridad, Gestión, Operación). |

---

## 🧠 Patrones de diseño aplicados

| Patrón | Dónde | Para qué |
|---|---|---|
| **Repository** | `IRepositorio<T>` y `Repositorio*` | Abstraer el acceso a datos detrás de una interfaz CRUD genérica |
| **Template Method** | `RepositorioBase<T>` | Escribir el CRUD una sola vez y que cada repositorio solo defina el SQL y el mapeo |
| **Singleton** | `RepositorioFactory`, `GestorSesion` | Una única instancia global de la fábrica y de la sesión activa |
| **Factory** | `RepositorioFactory`, `CodigoAccesoFactory` | Centralizar la creación de repositorios y de códigos de acceso |
| **Lazy Initialization** | Propiedades de `RepositorioFactory` | Crear cada repositorio recién cuando se usa por primera vez |
| **Service Layer** | `EntregaService`, `LoginService`, `LockerService`, … | Encapsular cada caso de uso y ocultarle a la UI los detalles de la DAL |
| **Layer Supertype** | `EntidadBase` | Base común para todas las entidades (`Id`, `EsNueva()`) |
| **Rich Domain Model** | `Locker`, `OrdenDeEntrega`, `CodigoAcceso` | Las entidades custodian sus propias reglas y transiciones de estado |
| **Result Object** | `ResultadoLogin`, `ResultadoReserva` | Devolver el resultado de una operación con su información asociada sin abusar de excepciones |
| **View Model / DTO** | `EstadoLockerVista`, `ResumenLockers` | Combinar datos de varias entidades para lo que necesita mostrar la pantalla |

<details>
<summary><b>🔍 Repository + Template Method</b></summary>

`RepositorioBase<T>` implementa `IRepositorio<T>` y define el **esqueleto** de todas las operaciones (abrir conexión, ejecutar el comando, recorrer el lector). Las subclases solo completan los "huecos":

```csharp
public abstract class RepositorioBase<T> : IRepositorio<T> where T : EntidadBase
{
    protected abstract string NombreTabla { get; }
    protected abstract string SqlSelect   { get; }
    protected abstract string SqlInsert   { get; }
    protected abstract T    Mapear(SqlDataReader lector);
    protected abstract void CargarParametros(SqlCommand comando, T entidad);

    public virtual IList<T> ObtenerTodos()
    {
        // el algoritmo es siempre el mismo; lo específico lo aportan las subclases
        ...
        while (lector.Read())
            lista.Add(Mapear(lector));
    }
}
```

Resultado: agregar una entidad nueva implica escribir su SQL y su mapeo, no un CRUD completo.
</details>

<details>
<summary><b>🔍 Singleton + Factory + Lazy Initialization</b></summary>

```csharp
public sealed class RepositorioFactory
{
    private static readonly RepositorioFactory _instancia = new RepositorioFactory();
    private RepositorioFactory() { }

    public static RepositorioFactory Instancia => _instancia;

    public RepositorioLocker Lockers
    {
        get
        {
            if (_lockers == null)
                _lockers = new RepositorioLocker();   // se crea recién al primer uso
            return _lockers;
        }
    }
}
```

`GestorSesion` sigue la misma idea: una única instancia que conoce al `UsuarioActual` y expone `ValidarRolAdministrador()` para que los servicios controlen permisos.
</details>

<details>
<summary><b>🔍 Factory de códigos de acceso</b></summary>

`CodigoAccesoFactory` encapsula todo lo necesario para crear un PIN seguro: generarlo con `RandomNumberGenerator` (no con `Random`), crear un salt, hashearlo y armar la entidad `CodigoAcceso`. El PIN en claro se devuelve **una única vez** para poder enviarlo por email; en la base solo queda su hash.
</details>

<details>
<summary><b>🔍 Rich Domain Model</b></summary>

```csharp
public void Reservar()
{
    if (Estado != EstadoLocker.Libre)
        throw new InvalidOperationException(
            "Solo se puede reservar un locker Libre. Estado actual: " + Estado + ".");

    Estado = EstadoLocker.Reservado;
}
```

Los servicios **coordinan**, pero no deciden qué transiciones son válidas. Así es imposible dejar un locker en un estado inconsistente desde cualquier parte del código.
</details>

---

## 🚚 Flujo: Reservar un locker (CU.Op.001)

```mermaid
sequenceDiagram
    actor Op as Operador
    participant UI as FrmReservarLocker
    participant S as EntregaService
    participant R as RepositorioFactory
    participant L as Locker
    participant F as CodigoAccesoFactory
    participant M as EmailService

    Op->>UI: Selecciona residente y tamaño
    UI->>S: ReservarLocker(idResidente, tamaño, descripción)
    S->>S: ValidarSesion()
    S->>R: Lockers.ObtenerLibreParaTamanio(edificio, tamaño)
    R-->>S: locker
    S->>L: Reservar()
    Note over L: Libre → Reservado<br/>(la entidad valida)
    S->>R: Lockers.Actualizar / Ordenes.Insertar
    S->>F: Generar(orden, Apertura)
    F-->>S: PIN en claro + CodigoAcceso (hash + salt)
    S->>R: Codigos.Insertar(codigo)
    S->>M: Enviar(correo del residente, PIN)
    M-->>S: ok / error (no revierte la reserva)
    S-->>UI: ResultadoReserva
    UI-->>Op: Muestra PIN y estado de la notificación
```

> 💡 Si el email falla, **la reserva sigue siendo válida**: la UI muestra el PIN para no perderlo y la notificación queda registrada como fallida.

---

## 🛡️ Seguridad

- **Contraseñas y PIN** hasheados con SHA-256 + **salt aleatorio** de 16 bytes por registro.
- **Comparación en tiempo constante** (`CryptographicOperations.FixedTimeEquals`) para evitar ataques de timing.
- **PIN generado con CSPRNG** (`RandomNumberGenerator`), de un solo uso y con vencimiento de 48 h.
- **Mensajes de login genéricos** ("Usuario o contraseña incorrectos") para no revelar qué usuarios existen.
- **Control de acceso por rol**: las operaciones de ABM exigen rol Administrador.

---

## 📁 Estructura del proyecto

```
Lockers_Inteligente/
├── LockersInteligentes.sln
├── Directory.Build.props            # Configuración común (net8.0, idioma, etc.)
└── src/
    ├── Dominio/                     # 🧩 Núcleo del negocio (sin dependencias)
    │   ├── Entidades/               #    Locker, OrdenDeEntrega, CodigoAcceso, Usuario, Rol,
    │   │                            #    Edificio, Residente, Repartidor, Notificacion, ...
    │   ├── Enums/                   #    EstadoLocker, EstadoOrden, TamanioPaquete, TipoCodigo, ...
    │   └── Arquitectura/            #    Idioma, Traduccion (multi-idioma)
    │
    ├── ArqBase/                     # 🧰 Servicios transversales
    │   ├── EncriptadorService.cs    #    Hash + salt + comparación segura
    │   ├── EmailService.cs          #    Notificaciones por SMTP
    │   └── Entidades/RegistroError.cs
    │
    ├── DAL/                         # 🗄️ Acceso a datos
    │   ├── Conexion.cs
    │   ├── Repositorios/            #    IRepositorio<T>, RepositorioBase<T>, RepositorioFactory
    │   │                            #    y un repositorio por entidad
    │   └── db/
    │       ├── 01_Esquema.sql       #    Creación de tablas, FKs y constraints
    │       └── 02_DatosIniciales.sql#    Roles, idiomas, traducciones y usuario admin
    │
    ├── BLL/                         # ⚙️ Lógica de negocio
    │   ├── LoginService.cs · GestorSesion.cs · SeguridadService.cs
    │   ├── EntregaService.cs · CodigoAccesoFactory.cs
    │   ├── LockerService.cs · EstadoLockerVista.cs
    │   └── EdificioService.cs · ResidenteService.cs · RepartidorService.cs · UsuarioService.cs
    │
    └── UI/                          # 🖥️ Windows Forms
        ├── Program.cs               #    Ciclo login → principal → cerrar sesión
        ├── FrmPrincipal.cs          #    Menú principal
        ├── Seguridad/               #    FrmLogin, FrmRegistro
        ├── Gestion/                 #    FrmEdificios, FrmLockers, FrmResidentes, FrmRepartidores, FrmUsuarios
        └── Operacion/               #    FrmEstadoLockers, FrmReservarLocker
```

---

## 🗃️ Modelo de datos (simplificado)

```mermaid
erDiagram
    EDIFICIO ||--o{ LOCKER : tiene
    EDIFICIO ||--o{ RESIDENTE : aloja
    LOCKER ||--o{ ORDEN_DE_ENTREGA : recibe
    RESIDENTE ||--o{ ORDEN_DE_ENTREGA : destinatario
    REPARTIDOR |o--o{ ORDEN_DE_ENTREGA : entrega
    ORDEN_DE_ENTREGA ||--o{ CODIGO_ACCESO : genera
    ROL ||--o{ USUARIO : asigna
    IDIOMA ||--o{ USUARIO : prefiere
    IDIOMA ||--o{ TRADUCCION : contiene
```

También incluye las tablas `Notificacion`, `Historial` (bitácora) y `Reporte`.

---

## 🚀 Cómo ejecutarlo

### Requisitos
- Windows 10/11
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (Express alcanza) y SSMS
- Visual Studio 2022 (recomendado)

### Pasos

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/LucianoDiazFranco/Lockers_Inteligente.git
   ```

2. **Crear la base de datos** ejecutando en orden:
   ```
   src/DAL/db/01_Esquema.sql
   src/DAL/db/02_DatosIniciales.sql
   ```

3. **Configurar la conexión** en `src/UI/App.config` si tu instancia no es `localhost\SQLEXPRESS`:
   ```xml
   <add name="LockersDb"
        connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=LockersInteligentes;Integrated Security=True;TrustServerCertificate=True;" />
   ```

4. **(Opcional) Configurar el envío de emails** agregando las claves `SmtpHost`, `SmtpPuerto`, `SmtpUsarSsl`, `SmtpRemitente`, `SmtpUsuario` y `SmtpPassword` en un archivo de secretos que **no se suba al repositorio**. Sin esto la reserva funciona igual, pero la notificación queda como fallida.

5. **Ejecutar** marcando `LockersInteligentes.UI` como proyecto de inicio:
   ```bash
   dotnet run --project src/UI
   ```

6. **Ingresar** con el usuario inicial de desarrollo: `admin` / `Admin1234` (cambialo después del primer ingreso).

---

## 🗺️ Próximos pasos

- [ ] Registrar entrega (CU.Op.002) validando el PIN de apertura
- [ ] Registrar retiro (CU.Op.003) con PIN de retiro
- [ ] Bitácora de operaciones (`TipoOperacion`)
- [ ] Reportes de uso y lockers vencidos
- [ ] Traducción completa de la interfaz desde la base de datos
- [ ] Backup y restore de la base

---

## 👥 Autores

- **Luciano Díaz Franco** – [@LucianoDiazFranco](https://github.com/LucianoDiazFranco)

<div align="center">
<sub>Hecho con ☕ y C#</sub>
</div>
