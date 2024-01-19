import { Component, OnInit} from '@angular/core';
import { DOCUMENT } from '@angular/common'; 
import { Inject }  from '@angular/core';
import { ApiService } from '../services/api.service';
import { interval, Subscription} from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers:[ApiService]
})

export class AppComponent implements OnInit{
  title = 'bancard-angular';
  SuccessProcessId:string='';
  subscription!: Subscription;
  statusQueryString: string | undefined;
  constructor(@Inject(DOCUMENT) document: Document, private service: ApiService) {
  
   }
  ngOnInit(): void {
  
  (<HTMLInputElement>document.getElementById("iframe-container")).style.visibility = "hidden";
  
  if(window.location.href.indexOf('payment_success')>0)
      alert("transaccion exitosa!");
  else if (window.location.href.indexOf('payment_fail')>0)
      alert("Error en transaccion"); 
  }
  async CheckSimpleBuy(){
    var shop_process_id = Number((<HTMLInputElement>document.getElementById("shop_process_id_get"))?.value);

      await this.service.CheckTransaction(shop_process_id).then((data:any)=>{
        (<HTMLInputElement>document.getElementById("resultMessageGet")).innerText = data.result;
        
      });
        
    
  }
  async SimpleBuy(){
    (<HTMLInputElement>document.getElementById("resultMessage")).innerText = '';
    var _shop_process_id = Number((<HTMLInputElement>document.getElementById("shop_process_id"))?.value);
    var _currency='PYG'; 
    var _amount =(<HTMLInputElement>document.getElementById("amount"))?.value;
    var _iva_amount = (<HTMLInputElement>document.getElementById("iva_amount"))?.value;
    var _additional_data="099VS ORO000046";
    var _description="Ejemplo de Pago";
    var _return_url=environment.return_url;
    var _cancel_url=environment.cancel_url;
    
    
      await this.service.SingleBuy(_shop_process_id, _currency, _amount??"0", _iva_amount??"0", _description, _return_url, _cancel_url,'')
      .then((res:any)=>{
        
        if(res.status==="error") {
        
          (<HTMLInputElement>document.getElementById("resultMessage")).innerText = res.messages[0].dsc;
         
        }
        else{
          this.SuccessProcessId = res.process_id;
            console.log("ProcessId: " + this.SuccessProcessId);
          const callJavascript = () => {
            window.BancardCheckoutCreateForm(this.SuccessProcessId);
          }
          callJavascript();
          (<HTMLInputElement>document.getElementById("iframe-container")).style.visibility = "visible";
             
        }
      }).catch((error:any)=>{
          console.log(error);
      });
      
     
    
  }
}
interface Window {
  BancardCheckoutCreateForm(process_id:string):any;
   
  }