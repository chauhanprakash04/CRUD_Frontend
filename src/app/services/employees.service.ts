import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Employees } from '../interfaces/employees';

@Injectable({
  providedIn: 'root'
})
export class EmployeesService {

  private apiUrl = "https://localhost:7017/api/Employees";
  constructor(private http: HttpClient) { }

  getEmployees(): Observable<Employees[]> {
    return this.http.get<Employees[]>(this.apiUrl);
  }

  getEmployee(id: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${id}`);
  }

  postEmployee(employee: Employees): Observable<Employees> {
    return this.http.post<Employees>(this.apiUrl, employee);
  }

  updateEmployee(id: number, employee: Employees): Observable<Employees> {
    return this.http.put<Employees>(`${this.apiUrl}/${id}`, employee);
  }

  deleteEmployee(id: number): Observable<Employees> {
    return this.http.delete<Employees>(`${this.apiUrl}/${id}`);
  }
 }
