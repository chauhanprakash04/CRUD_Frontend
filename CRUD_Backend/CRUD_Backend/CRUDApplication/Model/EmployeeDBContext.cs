using System.Data;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Npgsql;
using CRUDApplication.Repository;

namespace CRUDApplication.Model
{
    public class EmployeeDBContext : IEmployee
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EmployeeDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultCon");
        }

        public List<Employee> GetEmployees() 
        {
            List<Employee> employeeList = new List<Employee>();

            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from emp_tbl", con);
                NpgsqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())   
                {
                    Employee emp = new Employee();
                    emp.id = Convert.ToInt32(dr.GetValue(0).ToString());
                    emp.name = dr.GetValue(1).ToString();
                    emp.gender = dr.GetValue(2).ToString();
                    emp.age = Convert.ToInt32(dr.GetValue(3).ToString());
                    emp.salary = Convert.ToInt32(dr.GetValue(4).ToString());
                    emp.city = dr.GetValue(5).ToString();
                    employeeList.Add(emp);  
                }
            }

            return employeeList;
        }

        public Employee GetEmployeeById(int id)
        {
            using(NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("select * from emp_tbl where id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Employee emp = new Employee();
                    emp.id = dr.GetInt32(0);
                    emp.name = dr.GetString(1);
                    emp.gender = dr.GetString(2);
                    emp.age = dr.GetInt32(3);
                    emp.salary= dr.GetInt32(4);
                    emp.city= dr.GetString(5);  
                    return emp;
                }
                else
                {
                    return null;
                }

            }
        }

        public void AddEmployee(Employee emp)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                string query = "insert into emp_tbl (name, gender, age, salary, city) values (@name, @gender, @age, @salary, @city)";
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", emp.name);
                cmd.Parameters.AddWithValue("@gender", emp.gender); 
                cmd.Parameters.AddWithValue("@age", emp.age);
                cmd.Parameters.AddWithValue("@salary", emp.salary);
                cmd.Parameters.AddWithValue("@city", emp.city);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                con.Open();

                string query = "select update_employee(@id, @name, @gender, @age, @salary, @city)";

                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", emp.id);
                cmd.Parameters.AddWithValue("@name", emp.name);
                cmd.Parameters.AddWithValue("@gender", emp.gender);
                cmd.Parameters.AddWithValue("@age", emp.age);
                cmd.Parameters.AddWithValue("@salary", emp.salary);
                cmd.Parameters.AddWithValue("@city", emp.city);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public void DeleteEmployee(int id)
        {
            using(NpgsqlConnection con = new NpgsqlConnection(_connectionString))
            {
                string query = "select delete_employee(@id)";
                con.Open();

                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
