[System.Serializable]
public class Rootsignup
{
    public Usersignup user; 
}

[System.Serializable]
public class Usersignup
{
    public string name;
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
public class SignInResponse
{
    public RootsignupRes data;
}

[System.Serializable]
public class RootsignupRes
{
    public string message;
    public User_value user;
    public int status;
}

[System.Serializable]
public class User_value
{
    public int id;
    public string created_at;
    public string updated_at;
    public string email;
    public string name;
    public string role;
    public string city;
    public string country;
    public bool payment_status;
    public string payment_date;
    public bool vote_casted;
    public float total_collection;
}


