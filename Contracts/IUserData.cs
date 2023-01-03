﻿namespace AmiFlota.Contracts
{
    public interface IUserData
    {
        string Id { get; set; }
        string Name { get; set; }
        string Role { get; set; }

        bool IsPriviledgedUser();
    }
}
