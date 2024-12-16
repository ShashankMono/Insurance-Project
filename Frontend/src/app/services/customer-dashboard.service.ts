import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Policy } from '../models/policy';
import { PolicyAccount } from '../models/policy-account';

@Injectable({
  providedIn: 'root',
})
export class CustomerDashboardService {
  private url = 'https://localhost:7258/api';
  
  constructor(private http: HttpClient) {}

  getInstallmentTypes(): Observable<string[]> {
    return this.http.get<{ success: boolean; data: string[] }>(
      `${this.url}/CommonFeatures/installment_types`
    ).pipe(
      map((response) => response.data)
    );
  }

  createPolicyAccount(policyAccount: any): Observable<any> {
    return this.http.post(`${this.url}/PolicyAccount`, policyAccount);
  }

  
  saveDocument(documentData: any) {
    return this.http.post<any>(`${this.url}/PolicyAccountDocument`, documentData);
  }

  payInstallment(installmentId: string, customerId: string): Observable<any> {
    return this.http.put<any>(
      `${this.url}/PolicyInstallment/pay/${installmentId}`,
      { customerId }
    );
  }

  getPaymentSession(sessionData:any){
    return this.http.post<any>(`${this.url}/Payment/create-session`,sessionData);
  }

  getInstallment(accountId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/PolicyInstallment/policyaccount/${accountId}`);
  }

  postTransaction(id:any):Observable<any>{
    return this.http.get(`${this.url}/PolicyInstallment/pay/${id}`);
  }

  getCutomerByCustomerId(cutomerId:any):Observable<any>{
    return this.http.get(`${this.url}/Customer/${cutomerId}`);
  }

  getCustomerDetails(userId: string): Observable<any> {
    return this.http.get(`${this.url}/Customer/User/${userId}`);
  }

  updateCustomerDetails(customer: any): Observable<any> {
    return this.http.put(`${this.url}/Customer`, customer);
  }

  getCustomerAccounts(): Observable<any> {
    return this.http.get(`${this.url}/Customer`);
  }
  
  addNominee(nominee: any): Observable<any> {
    return this.http.post(`${this.url}/Nominee`, nominee);
  }
  
  updateNomine(nominee:any):Observable<any>{
    return this.http.put(`${this.url}/Nominee`,nominee);
  }
  getNomineesByCustomerId(customerId: string,policyAccountId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/Nominee/customer/${customerId}/policyAccount/${policyAccountId}`);
  }
  
  deleteNominee(nomineeId: string): Observable<any> {
    return this.http.delete(`${this.url}/Nominee/${nomineeId}`);
  }
  addPolicyAccountDocument(document: any) {
    return this.http.post<any>(`${this.url}/PolicyAccountDocument`, document);
  }
  
  updatePolicyAccountDocument(documentData:any): Observable<any> {
    return this.http.put<any>(`${this.url}/PolicyAccountDocument`, {
      documentData
    });
  }
  
  deletePolicyAccountDocument(documentId: string) {
    return this.http.delete<any>(`${this.url}/PolicyAccountDocument/${documentId}`);
  }

  updateCustomerStatus(data:any):Observable<any>{
    return this.http.post<any>(`${this.url}/Customer/approve`,data);
  }

  getTransactionHistory(customerId: string): Observable<any> {
    return this.http.get(`${this.url}/Transaction/${customerId}`);
  }

  withdrawClaim(policyAccountId: string): Observable<any> {
    return this.http.delete(`${this.url}/Claim/${policyAccountId}`);
  }
  registerCustomer(customerData: any): Observable<any> {
    return this.http.post<any>(`${this.url}/Customer`, customerData);
  }

  getAllCustomers(pageNumber: number, pageSize: number, searchQuery: string): Observable<any> {
    console.log(pageSize);
    let params = new HttpParams()
          .set('pageNumber', pageNumber.toString())
          .set('pageSize', pageSize.toString());
    
        if (searchQuery) {
          params = params.set('searchQuery', searchQuery);
        }

    return this.http.get(`${this.url}/Customer`);
  }
}
