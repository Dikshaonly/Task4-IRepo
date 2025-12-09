using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dapper;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Data.SqlClient;
using Task4.Models;

namespace Task4.Repository{
    public class ErrorRepository : IErrorRepository{

        public string GetErrorMessage(Exception ex){
            if(ex is SqlException){
                return "Database error.Please try again";
            }
            else if(ex is DivideByZeroException){
                return "Cannot Divide by Zero!";
            }
            else if(ex is ArgumentNullException){
                return "Missing Required Information!";
            }
            else{
                return "Something went Wrong.Please try again!";
            }
        }

        public void ShowError(ITempDataDictionary tempData,Exception ex){
            string message = GetErrorMessage(ex);
            tempData["ErrorMessage"] = message;
        }

        public void ShowSuccess(ITempDataDictionary tempData,string message){
            tempData["SuccessMessage"] = message;
        }
    }
}