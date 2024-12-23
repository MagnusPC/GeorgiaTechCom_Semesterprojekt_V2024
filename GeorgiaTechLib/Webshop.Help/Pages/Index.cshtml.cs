﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Webshop.Help.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private string MSconnectionString; //the server connectionstring without database
        private string PGconnectionString; //the connectionstring with database
        private string MSserver = "localhost";
        private string PGserver = "localhost";

        private List<string> Errors = new List<string>();

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            this.MSconnectionString = config.GetConnectionString("MSConnection");
            this.PGconnectionString = config.GetConnectionString("PGConnection");
            string MSnewServer = Environment.GetEnvironmentVariable("MSSERVER");
            string PGnewServer = Environment.GetEnvironmentVariable("PGSERVER"); 
            Console.WriteLine($"MSSERVER: {MSnewServer}, PGSERVER: {PGnewServer}");
            System.Console.WriteLine("New server: " + PGnewServer);

            if (!string.IsNullOrEmpty(PGnewServer))
            {
                this.MSserver = MSnewServer;
                this.PGserver = PGnewServer;
                
            }

            this.MSconnectionString = this.MSconnectionString.Replace("{server}", this.MSserver);
            this.PGconnectionString = this.PGconnectionString.Replace("{server}", this.PGserver);
            System.Console.WriteLine("New server: " + this.PGconnectionString);
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {

            // Postgres 
            DatabaseService.InitializeDatabase(this.PGconnectionString);

            //create the database
            this.MSconnectionString = this.MSconnectionString + ";database=master";
            CreateDatabase();
            CreateReviewDatabase();//creating psureviews database
            this.MSconnectionString = this.MSconnectionString + ";database=psuwebshop"; //make sure they are created in the right database
            CreateCategoryTable();
            CreateCustomerTable();
            CreateProductTable();
            CreateProductCategoryTable();
            this.MSconnectionString = this.MSconnectionString + ";database=PSUReviews"; //make sure they are created in the right database
            CreateReviewsTable();
           
            

            TempData["errors"] = Errors;
            return Redirect("/?seed=1");
        }

        private void CreateDatabase()
        {            
            ExecuteSQL("CREATE DATABASE psuwebshop", this.MSconnectionString);           
        }

        private void CreateReviewDatabase()
        {
            ExecuteSQL("CREATE DATABASE PSUReviews", this.MSconnectionString);
        }

        private void CreateReviewsTable()
        {
            string sql = @"CREATE TABLE [dbo].[Reviews](
	        [Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	        [ProductId] [int] NOT NULL,
	        [UserId] [int] NOT NULL,
	        [Comment] [nvarchar](max) NOT NULL,
	        [Rating] [int] NOT NULL,
	        [Created] [datetime] NOT NULL)";

            string alterSql = "ALTER TABLE [dbo].[Reviews] ADD  DEFAULT (getdate()) FOR [Created]";
            
            ExecuteSQL(sql, this.MSconnectionString);
            ExecuteSQL(alterSql, this.MSconnectionString);
        }

        private void CreateCategoryTable()
        {
            string sql = "CREATE TABLE Category(" +
            "[Id] [int] IDENTITY(1,1) NOT NULL," +
            "[Name] [nvarchar](150) NOT NULL," +
            "[ParentId] [int] NOT NULL," +
            "[Description] [ntext] NOT NULL," +
            "CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED " +
            "(" +
            "[Id] ASC" +
            ")" +
            ")";
            ExecuteSQL(sql, this.MSconnectionString);
        }

        private void CreateCustomerTable()
        {
            string sql = "CREATE TABLE Customer(" +
            "[Id] [int] IDENTITY(1,1) NOT NULL," +
            "[Name] [nvarchar](150) NOT NULL," +
            "[Address] [nvarchar](200) NOT NULL," +
            "[Address2] [nvarchar](200) NULL," +
            "[City] [nvarchar](200) NOT NULL," +
            "[Region] [nvarchar](200) NOT NULL," +
            "[PostalCode] [nvarchar](50) NOT NULL," +
            "[Country] [nvarchar](150) NOT NULL," +
            "[Email] [nvarchar](100) NOT NULL," +
            "CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED " +
            "(" +
            "[Id] ASC" +
            ")" +
            ")";
            ExecuteSQL(sql, this.MSconnectionString);
        }

        private void CreateProductTable()
        {
            string sql = "CREATE TABLE Product(" +
            "[Id] [int] IDENTITY(1,1) NOT NULL," +
            "[Name] [nvarchar](150) NOT NULL," +
            "[SKU] [nvarchar](50) NOT NULL," +
            "[Price] [int] NOT NULL," +
            "[Currency] [nvarchar](3) NOT NULL," +
            "[Description] [ntext] NULL," +
            "[AmountInStock] [int] NULL," +
            "[MinStock] [int] NULL," +
            "CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED " +
            "(" +
            "[Id] ASC" +
            ")" +
            ")";
            ExecuteSQL(sql, this.MSconnectionString);
        }

        private void CreateProductCategoryTable()
        {
            string sql = "CREATE TABLE ProductCategory(" +
            "[ProductId] [int] NOT NULL," +
            "[CategoryId] [int] NOT NULL," +
            "CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED " +
            "(" +
            "[ProductId] ASC," +
            "[CategoryId] ASC" +
            ")" +
            ")";
            ExecuteSQL(sql, this.MSconnectionString);
        }

        private void ExecuteSQL(string sql, string localConnectionString)
        {
            try
            {
                Console.WriteLine("Connection: " + localConnectionString);
                using (SqlConnection connection = new SqlConnection(localConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            } catch(Exception ex)
            {
                Errors.Add(ex.Message);
            }
        }
    }
}