import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  
  baseApiURL = "";
  publicKey = "";
  privateKey = "";
  headers = new HttpHeaders;
  constructor(private http: HttpClient) {
    this.baseApiURL = environment.baseApiURL;
    this.publicKey = environment.publicKey;
    this.privateKey = environment.privateKey;
    this.headers = new HttpHeaders({ 
      'Access-Control-Allow-Origin': '*',
      'Content-Type': 'application/json; charset=UTF-8',
   });
  }

  async SingleBuy(_shop_process_id:number, _currency:string, _amount:string, _iva_amount:string, 
    _description:string, _return_url:string, _cancel_url:string, _additional_data?:string|null) {
    let simpleBuyModel = {
      shop_process_id: _shop_process_id,
      currency: _currency,
      amount: _amount,
      iva_amount: _iva_amount,
      additional_data: '',
      description: _description,
      return_url: _return_url,
      cancel_url: _cancel_url
    };
              
    return this.postData(simpleBuyModel, this.baseApiURL + "singlebuy");
  }

  async postData(data: any, url:string): Promise<any>  {
    let options= { headers: this.headers,};
    return await this.http.post<any[]>(url,data,options).toPromise();
  }

  public async CheckTransaction(idtransaction:number): Promise<any> {
    let options= { headers: this.headers};
    var res = await this.http.get<any[]>(this.baseApiURL +'singlebuygetconfirmations/'+ idtransaction.toString(), options ).toPromise();
     
     return res;
     /*
   } catch (error) {
     console.error('Error fetching data from the API:', error);
     throw error;
   }
   */
  }
}
