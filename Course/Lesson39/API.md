# API metods

Певучих методов опись,

В том коде мудрец ковыряет,

Как лира звучит он розыгрышь,

И слова в танце плывут, смелы.

## TaskController

### GetAll

На пути 'tasks/getall', гнетущий запрос,

Отвечает тихо, в сердце, нежно,

Всем записям список дарует,

Из таблицы, как сон, безмерно.

```cs
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        return Ok(_taskManager.GetAllTasks());
    }
```
### Get(int id)
На стезе 'tasks/get/{id}' блеск ключа,

Ищет он, как птица в свете,

Запись по ID, вот тут луча,

Дарует тайну в ответе.
```cs
    [HttpGet("get/{id}")]

    public TrackerTask Get(int id)
    {
        return _taskManager.GetTaskById(id);
    }
```
### Delete(int id)
Там, где 'tasks/delete/{id}' - тихий мрак,

Удаляет, словно ветер,

По ключу ID запись, как страх,

Распуская в себе цветы.
```cs
    [HttpDelete("delete/{id}")]
    public void Delete(int id)
    {  
        _taskManager.DeleteTask(id); 
    }
```
### Create([FromBody] TrackerTask task)
На мгновенье в 'add', звучит мольба,

В таблицу добавляет новый след,

Как перо, легко, словно вальс, вольно,

Словно судьбу творит - вновь, свет.
```cs
    [HttpPost("add")]
    public void Create([FromBody] TrackerTask task)
    {
        _taskManager.AddTask(task);
    }
```

### AddRandom()
На пути 'tasks/addrandom/{id}' вихрь,

Новый след рисует случайным мигом,

Словно звезды в ночи, ввысь, вспыхнув,

Имям светит, словно звук и миг.

```cs
    [HttpGet("addrandom/{id}")]
    public void User(int id)
    {
         for(int x = 0 ; x < id;x++ )
         {
            int lastTaskID = 0 ;
            try
            {
                var tasks = _taskManager.GetAllTasks(); 
                lastTaskID = (int)tasks.Max(t => t.ID);   
            } 
            catch
            {
                lastTaskID = 0; 
            }

            var newTask = new TrackerTask();
            var randomName = "Task #" + (lastTaskID + x).ToString();
            newTask.ID = lastTaskID + x;       
            newTask.Name = randomName;  
            newTask.Description = "This is a random task";   
            newTask.DueDate = new DateTime();
            newTask.AssignedUser = new User {Name = "random", Email = "random@random.com", Password = "random", ID = 1};
            _taskManager.AddTask(newTask); 
         }
    }
```
---
## AccountController

### Create([FromBody] User account)
На миге 'api/account/register', рожденье,

Новый пользователь, как звезда в вышине,

В таблицу легко, как ветер, пенье,

Имям светит, словно рассвет в плени.
```cs
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

### Login([FromBody] User account)
На пути 'api/account/login', встреча,

Светом правильных имен и паролей,

В аккаунт входит он, как в реку, вечность,

Словно сны во мгле тьмы и полей.
```cs
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

### Logout()
На тропе 'api/account/logout', сон вечный,

От аккаунта выходит он, как в туман,

Словно солнце в застенках затворенных,

Светом лучей вновь светит он, как рассвет встан.
```cs
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync().Wait();
        return Ok();
    }
```

### GetAccounts()
На стезе 'api/account/account', зовет,

Список всех, словно звезды, сверкая,

Зарегистрированных, как сон, летит,

Именами своими мир оживляя.

```cs
    [HttpGet("account")]
    public List<User> GetAccounts()
    {
        return _userManager.Users.Select(u => new User { Name = u.UserName, Email = u.Email }).ToList();
    }
```

