﻿using System;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Data.Model;

namespace EmployeeManagementSystem.Services
{
	public class EmployeeService
	{
        private readonly ApplicationDbContext _db;
        public EmployeeService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Employee>> GetEmployeeAsync()
        {
            return await _db.Employees.OrderBy(x => x.FirstName).ToListAsync();
        }

        public async Task<Employee> GetEmployeeDetailsAsync(int id)
        {
            return await _db.Employees.Where(x => x.EmpID == id).FirstOrDefaultAsync();
        }

        public async Task<Employee> AddEmployee(Employee newEmployee)
        {
            _db.Employees.Add(newEmployee);
            await _db.SaveChangesAsync();
            return newEmployee; // Not used, just something to return for now
        }

        public async Task<Employee> DeleteEmployee(int Id)
        {
            var prov = await _db.Employees
                .Where(x => x.EmpID == Id)
                .FirstOrDefaultAsync();
            _db.Employees.Remove(prov);
            await _db.SaveChangesAsync();
            return prov;
        }

        public async Task<Employee> UpdateEmployee(int Id, string firstName, string lastName,
           int age, string email, string phone, DateTime createDate, DateTime hireDate, DateTime terminationDate, string employementStatus)
        {
            var avail = await _db.Employees.Where(x => x.EmpID == Id).FirstOrDefaultAsync();
            avail.FirstName = firstName;
            avail.LastName = lastName;
            avail.Age = age;
            avail.Email = email;
            avail.Phone = phone;
            avail.CreateDate = createDate;
            avail.HireDate = hireDate;
            avail.TerminationDate = terminationDate;
            avail.EmploymentStatus = employementStatus;
            await _db.SaveChangesAsync();
            return avail;
        }

        public async Task UpdateDatabaseAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}

