import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Employees } from 'src/app/interfaces/employees';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  modalTitle: string = 'Employee Form';
  employees: Employees[] = [];
  empForm!: FormGroup;
  result: any = [];

  constructor(private employeeService: EmployeesService, private http: HttpClient, private fb: FormBuilder) { }

  ngOnInit(): void {
    this.empForm = this.fb.group({
      name: ['', Validators.required],
      gender: ['', Validators.required],
      age: ['', Validators.required],
      salary: ['', Validators.required],
      city: ['', Validators.required]
    })
    this.fetchEmployees()
  }

   fetchEmployees(): void {
    this.employeeService.getEmployees().subscribe({
      next: ((res: Employees[]) => {
        this.employees = res;
      }),
      error: ((err: any) => {
        console.error(err);
      })
    });
  }

  getDetails(id:number) {
    this.employeeService.getEmployee(id).subscribe({
      next: ((res: any) => {
        this.result = res;
        // console.log(this.result.name);
      }),
      error: ((err: any) => {
        console.error(err);
      })
    });
  }

  selectedEmployee: Employees | null = null;

  openEditModal(employee: Employees): void {
    this.modalTitle = 'Edit Employee';
    this.selectedEmployee = { ...employee };
    this.empForm.patchValue(this.selectedEmployee);
  }

  onSubmit() {
    if (this.selectedEmployee) {
      const updatedEmployee = { ...this.selectedEmployee, ...this.empForm.value };

      this.employeeService.updateEmployee(this.selectedEmployee.id, updatedEmployee).subscribe({
        next: () => {
          // console.log("Employee updated");
          this.fetchEmployees();
        },
        error: (err: any) => {
          console.error(err)
        }
      })
    }
    else {
      const data = this.empForm.value;
      this.employeeService.postEmployee(data).subscribe({
        next: () => {
          this.fetchEmployees();
          this.empForm.reset();
        },
        error: ((err: any) => {
          console.error(err);
        })
      });
    }
    this.empForm.reset();
    this.selectedEmployee = null;
  }

  onDelete(id: number): void {
    if (confirm("Are sure you want to delete this employee?")) {
      this.employeeService.deleteEmployee(id).subscribe({
        next: () => {
          // console.log("Employee deleted");
          this.employees = this.employees.filter(emp => emp.id !== id);
        },
        error: (err: any) => {
          console.error(err);
        }
      })
    }
  }

  onClose(): void {
    this.modalTitle = 'Employee Form'
    this.empForm.reset();
  }
}
