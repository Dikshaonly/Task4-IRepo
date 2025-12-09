using System;
using System.Collections.Generic;
using Task4.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Task4.Repository{
    public interface IErrorRepository{
        string GetErrorMessage(Exception ex);

        void ShowError(ITempDataDictionary tempData,Exception ex);

        void ShowSuccess(ITempDataDictionary tempData,string message);
    }
}