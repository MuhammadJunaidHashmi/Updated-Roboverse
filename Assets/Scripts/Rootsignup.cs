[System.Serializable]
public class Rootsignup
{
    public Usersignup user; 
}

[System.Serializable]
public class Usersignup
{
    public string email;
    public string password;
    public string password_confirmation;
}


[System.Serializable]
public class Rootsignin
{
    public Usersignin user;
}
[System.Serializable]
public class Usersignin
{
    public string email;
    public string password;
}

[System.Serializable]
public class RootsignupRes
{
    public string message;
    public int status;
    public UsersignupRes user;
}

[System.Serializable]
public class UsersignupRes
{
    public int id;
    public string email;
    public string created_at;
    public string updated_at;
}


