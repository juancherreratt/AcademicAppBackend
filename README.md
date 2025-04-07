# AcademicAppBackend

Backend desarrollado en ASP.NET Core con Clean Architecture para la gestión académica de estudiantes, profesores y materias.

## 📆 Clonación del repositorio

```bash
git clone [https://github.com/juancherreratt/AcademicAppBackend.git](https://github.com/juancherreratt/AcademicAppBackend.git)
cd AcademicAppBackend
```

---

## ⚙️ Aplicar migraciones y actualizar la base de datos

### 1. Crear migración inicial

> Asegúrate de tener instalada la herramienta `dotnet-ef`. Si no la tienes, instálala con:

```bash
dotnet tool install --global dotnet-ef
```

Luego, ejecuta:

```bash
dotnet ef migrations add InitialCreate -p .\src\Infrastructure\ -s .\src\WebApi\
```

### 2. Aplicar las migraciones a la base de datos

```bash
dotnet ef database update -p .\src\Infrastructure\ -s .\src\WebApi\
```

---

## 🌱 Seeders (Datos de prueba)

Durante el inicio de la aplicación, se ejecuta automáticamente el `DbSeeder` ubicado en:

```
Infrastructure/Identity/Seeds/DbSeeder.cs
```

Este se encarga de:

- Crear los roles `Student` y `Teacher`.
- Crear 5 profesores con usuarios (`teacher1@school.com` a `teacher5@school.com`).
- Crear 10 materias, asignando 2 por cada profesor.
- Crear 5 estudiantes con usuarios (`student1@school.com` a `student5@school.com`).

### Credenciales por defecto

- **Email**: `teacher1@school.com`
- **Contraseña**: `Password123!`

- **Email**: `student1@school.com`
- **Contraseña**: `Password123!`

---

## 🚀 Iniciar el servidor

Desde la carpeta raíz del proyecto, ejecuta:

```bash
cd src/WebApi
dotnet run
```

El servidor quedará disponible en: `https://localhost:5001` o `http://localhost:5000`

---

## 🧩 Estructura de carpetas

```
AcademicAppBackend/
├── src/
│   ├── Core/               # Entidades, interfaces y lógica de dominio
│   ├── Infrastructure/     # Repositorios, contexto EF, configuración de identidad
│   └── WebApi/             # API principal y configuración de dependencias
```

---

