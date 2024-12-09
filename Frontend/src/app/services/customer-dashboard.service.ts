import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Policy } from '../models/policy';

@Injectable({
  providedIn: 'root',
})
export class CustomerDashboardService {
  private url = 'https://localhost:7258/api';
  
  constructor(private http: HttpClient) {}

  getPolicies(): Observable<any[]> {
    return this.http.get<{ data: any[] }>(`${this.url}/Policy`).pipe(
      map((response) => response.data)
    );
  }

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
  getPolicyAccounts(): Observable<any> {
    return this.http.get<any>(`${this.url}/PolicyAccount`);
  }
  uploadFile(fileData: FormData) {
    return this.http.post<any>(`${this.url}/FileUpload`, fileData);
  }
  
  saveDocument(documentData: any) {
    return this.http.post<any>(`${this.url}/PolicyAccountDocument`, documentData);
  }
  
  

  cancelPolicyAccount(policyAccountId: string): Observable<any> {
    return this.http.put<any>(
      `${this.url}/PolicyAccount/cancel/${policyAccountId}`,
      {}
    );
  }

  claimPolicy(policyAccountId: string, claimData: any): Observable<any> {
    return this.http.post<any>(
      `${this.url}/PolicyAccount/claim/${policyAccountId}`,
      claimData
    );
  }

  payInstallment(installmentId: string, customerId: string): Observable<any> {
    return this.http.put<any>(
      `${this.url}/PolicyInstallment/pay/${installmentId}`,
      { customerId }
    );
  }

  getPaymentSession(sessionData:any){
    return this.http.post<any>(`${this.url}/Payment/create-session`,sessionData)
  }

  getInstallment(accountId:any):Observable<any>{
    return this.http.get<any>(`${this.url}/PolicyInstallment/policyaccount/${accountId}`);
  }

  postTransaction(id:any):Observable<any>{
    return this.http.get(`${this.url}/PolicyInstallment/pay/${id}`);
  }

  // Profile
  getProfile(customerId: string): Observable<any> {
    return this.http.get(`${this.url}/Customer/${customerId}`);
  }

  updateProfile(customerId: string, profileData: any): Observable<any> {
    return this.http.put(`${this.url}/Customer/${customerId}`, profileData);
  }

  submitQuery(queryText: string): Observable<any> {
    return this.http.post(`${this.url}/query`, { text: queryText });
  }
  
  getQueries(): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/query`);
  }
  
  
  
  addNominee(nominee: any): Observable<any> {
    return this.http.post(`${this.url}/Nominee`, nominee);
  }
  
  getNomineesByCustomerId(customerId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.url}/Nominee/${customerId}`);
  }
  
  deleteNominee(nomineeId: string): Observable<any> {
    return this.http.delete(`${this.url}/Nominee/${nomineeId}`);
  }
  addPolicyAccountDocument(document: any) {
    return this.http.post<any>(`${this.url}/PolicyAccountDocument`, document);
  }
  getPolicyAccountDocuments(policyAccountId: string) {
    return this.http.get<any>(`${this.url}/PolicyAccountDocument/${policyAccountId}`);
  }
  
  updatePolicyAccountDocument(document: any) {
    return this.http.put<any>(`${this.url}/PolicyAccountDocument`, document);
  }
  
  deletePolicyAccountDocument(documentId: string) {
    return this.http.delete<any>(`${this.url}/PolicyAccountDocument/${documentId}`);
  }
  

  // Transaction History
  getTransactionHistory(customerId: string): Observable<any> {
    return this.http.get(`${this.url}/Transaction/${customerId}`);
  }

  // Withdraw Claim
  withdrawClaim(policyAccountId: string): Observable<any> {
    return this.http.delete(`${this.url}/Claim/${policyAccountId}`);
  }
  registerCustomer(customerData: any): Observable<any> {
    return this.http.post<any>(`${this.url}/Customer`, customerData);
  }
}
