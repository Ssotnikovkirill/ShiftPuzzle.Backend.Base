

using System.Security.Cryptography.X509Certificates;

public class AccountManager : IAccountManager
{
    private readonly AccountContext _context;

    public static User CurrentUser = new() { ID=-1, Name="empty", Password="empty" };

    public AccountManager(AccountContext context)
    {
        _context = context;
    }

    public void RegisterAccount(User account)
    {
        Console.WriteLine("[Account manager] registring account: " + account.Name);

        
        Console.WriteLine(_context.Users.Any(u => u.Name == account.Name));

        // foreach (User user in _context.Users)
        // {
        //     Console.WriteLine(user.Name);
        //     Console.WriteLine(account.Name);
        //     Console.WriteLine(user.Name == account.Name);
        // }
        string[] domens = { "ru", "com" };

        if (account.Name.Contains('@')
            && account.Name.Contains('.')
            && account.Name.Length > 5
            && account.Name.Length < 50
            && domens.Any(d => account.Name.Split('.').Last().Contains(d))
            )
        {
            account.Email = account.Name;
            account.Name = account.Name.Split('@').First();
            Console.WriteLine($"Email of {account.Name} is valid");
        }
        else
        {
            account.Email = account.Name;
        }

        if (_context.Users.Any(u => u.Name == account.Name))
        {
            Console.WriteLine("Account with name " + account.Name + " already exists.");
            Logger("user tried to enter into account");
            return;
        }

        account.ID = _context.Users.Count() + 1;
        _context.Users.Add(account);
        _context.SaveChanges();

        CurrentUser = account;
        AccountManager.Logger("account created");
    }

    public User GetAccount(string accountName)
    {
        return _context.Users.FirstOrDefault(u => u.Name == accountName);
    }

    public List<User> GetAccounts()
    {
        return _context.Users.ToList();
    }

    public bool VerifyAccount(User account)
    {
        if (_context.Users.Any(u => u.Name == account.Name && u.Password == account.Password))
        {
            CurrentUser = account;
            Console.WriteLine("Account verified.");
            Logger("account verified");
            return true;
        }
        else
        {
            Console.WriteLine("Account not verified.");
            Logger("account not verified");
            return false;
        }
    }

    public static void Logger(string action)
    {
        string logMessage = $"{CurrentUser.Name},{CurrentUser.Password},{DateTime.Now},{action}\n";
        File.AppendAllText("ActionsLog.csv", logMessage);
    }
}