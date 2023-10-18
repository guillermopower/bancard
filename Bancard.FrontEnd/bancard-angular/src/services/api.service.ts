import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private SimpleBuyapiUrl:string='';
  private Environment = "https://localhost:7114/api/bancard/";
  private BaseAddress = "singlebuy";
  private publicKey = "1UFiKuPqgccfTi3XX9iAA6Vt9Oa4dD63";
  private privateKey = "0gRRUhowsFtSZJnSBdL3F+dvEq1k96mAbR0XppX.";
  constructor(private http: HttpClient) {
  
    this.SimpleBuyapiUrl = this.Environment + this.BaseAddress;
    
  }

  async SingleBuy(_shop_process_id:number, _currency:string, _amount:string, _iva_amount:string, _additional_data:string, 
    _description:string, _return_url:string, _cancel_url:string) {
    let simpleBuyModel = {
  shop_process_id: _shop_process_id,
  currency: _currency,
  amount: _amount,
  iva_amount: _iva_amount,
  additional_data: _additional_data,
  description: _description,
  return_url: _return_url,
  cancel_url: _cancel_url
};
              
    return this.postData(simpleBuyModel, this.SimpleBuyapiUrl);
  }

  async postData(data: any, url:string): Promise<any>  {
   
   let _headers = new HttpHeaders({ 
    'Access-Control-Allow-Origin': '*',
    'Content-Type': 'application/json; charset=UTF-8',
 });
 
 let options= { 
  headers: _headers,
  
 };
    return await this.http.post<any[]>(url,data,options).toPromise();
      
    
  }
}
