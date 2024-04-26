## Task Tracker API - README.md

# Описание

Этот проект представляет собой API для приложения Task Tracker, позволяющего управлять задачами и учетными записями пользователей. 

## Функциональность

* Управление задачами: 
    * Просмотр списка всех задач.
    * Просмотр информации о конкретной задаче.
    * Создание новых задач.
    * Удаление задач.
    * Генерация случайных задач.
* Управление учетными записями:
    * Регистрация новых пользователей.
    * Вход в систему.
    * Выход из системы.
    * Просмотр списка зарегистрированных пользователей.

## Инструкция по установке

1. Клонируйте репозиторий: [GitHub](https://github.com/AyyGee18/ShiftPuzzle.Backend.Base/tree/dev_Sanjar_Satlykov)
2. Настройте базу данных и другие параметры в файле `Program.cs`.
3. Запустите приложение.

## Использование API
### Примеры кода

#### TaskController

* Получение списка всех задач:

GET /tasks/getall
```c#
    [HttpGet("/api/tasks/getall")]
    public IEnumerable<TrackerTask> GetAll()
    {
        return _taskManager.GetAllTasks();
    }
```


* Создание новой задачи:

POST /tasks/add
Content-Type: application/json

```c#
    [HttpPost("/api/tasks/add")]
    public void Create([FromBody] TrackerTask task)
    {
        _taskManager.AddTask(task);
    }
```

* Удаление задачи:

DELETE /tasks/delete/123
```c#
    [HttpGet("/api/tasks/delete/{id}")]
    public void Delete(int id)
    {  
        _taskManager.DeleteTask(id); 
    }
```

#### AccountController

* Регистрация нового пользователя:

POST /api/account/register
Content-Type: application/json

{
  "Name": "username",
  "Email": "user@example.com",
  "Password": "password"
}
```c#
    [HttpPost("register")]
    public IActionResult Create([FromBody] User account)
    {
        var user = new IdentityUser { UserName = account.Name, Email = account.Email};
        var result = _userManager.CreateAsync(user, account.Password).Result;
        if (result.Succeeded)
        {
            _signInManager.SignInAsync(user, false).Wait();
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
```

* Вход в систему:

POST /api/account/login
Content-Type: application/json

{
  "Name": "username",
  "Password": "password"
}

```c#
    [HttpPost("login")]
    public IActionResult Login([FromBody] User account)
    {
        var result = _signInManager.PasswordSignInAsync(account.Name, account.Password,false, false).Result;
        if (result.Succeeded)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

```



## Авторы

* [VK](https://vk.com/satlykovs)


## Связь

* satlykovs@gmail.com