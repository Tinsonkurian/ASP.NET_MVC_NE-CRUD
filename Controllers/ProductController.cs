using CRUD.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Linq;



namespace CRUD.web.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = "Data Source=Server name;Initial Catalog=Database name;Integrated Security=True;Trust Server Certificate=True";


        // GET: ProductController
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT Id, Name, Price FROM Product";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Product product = new Product
                            {
                                Id = Convert.ToInt32(rdr["Id"]),
                                Name = rdr["Name"].ToString(),
                                Price = Convert.ToDecimal(rdr["Price"])
                            };
                            products.Add(product);
                        }
                    }
                }
            }
            return View(products);
         
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT Id, Name, Price FROM Product WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            product.Id = Convert.ToInt32(rdr["Id"]);
                            product.Name = rdr["Name"].ToString();
                            product.Price = Convert.ToDecimal(rdr["Price"]);
                        }
                    }
                }
            }
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
           
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string sqlQuery = "INSERT INTO Product (Name, Price) VALUES (@Name, @Price)";
                        using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@Name", product.Name);
                            cmd.Parameters.AddWithValue("@Price", product.Price);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(product);

            }
            catch
            {
                return View("Index");
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT Id, Name, Price FROM Product WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            product.Id = Convert.ToInt32(rdr["Id"]);
                            product.Name = rdr["Name"].ToString();
                            product.Price = Convert.ToDecimal(rdr["Price"]);
                        }
                    }
                }
            }
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
            
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string sqlQuery = "UPDATE Product SET Name = @Name, Price = @Price WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@Name", product.Name);
                            cmd.Parameters.AddWithValue("@Price", product.Price);
                            cmd.Parameters.AddWithValue("@Id", product.Id);
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    return RedirectToAction("Index");
                }
                return View(product);
               
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT Id, Name, Price FROM Product WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            product.Id = Convert.ToInt32(rdr["Id"]);
                            product.Name = rdr["Name"].ToString();
                            product.Price = Convert.ToDecimal(rdr["Price"]);
                        }
                    }
                }
            }
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
            
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = "DELETE FROM Product WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index");
 
            }
            catch
            {
                return View();
            }
        }
    }
}
