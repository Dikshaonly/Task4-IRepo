using System;
using System.Collections.Generic;
using Task4.Models;

namespace Task4.Repository{
   public interface IConnectionRepository{
    string GetCS(string connectionId = "DefaultConnection");
   }
}