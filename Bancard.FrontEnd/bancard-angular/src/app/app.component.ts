import { Component, OnInit} from '@angular/core';
import { DOCUMENT } from '@angular/common'; 
import { Inject }  from '@angular/core';
import { ApiService } from '../services/api.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers:[ApiService]
})

export class AppComponent implements OnInit{
  title = 'bancard-angular';
  SuccessProcessId:string='';
  
  constructor(@Inject(DOCUMENT) document: Document, private service: ApiService) {
  
   }
  ngOnInit(): void {
  
  (<HTMLInputElement>document.getElementById("iframe-container")).style.visibility = "hidden";
  }
  async SimpleBuy(){
    (<HTMLInputElement>document.getElementById("resultMessage")).innerText = '';
    var _shop_process_id = Number((<HTMLInputElement>document.getElementById("shop_process_id"))?.value);
    var _currency='PYG'; 
    var _amount =(<HTMLInputElement>document.getElementById("amount"))?.value;
    var _iva_amount = (<HTMLInputElement>document.getElementById("iva_amount"))?.value;
    var _additional_data="099VS ORO000046";
    var _description="Ejemplo de Pago";
    var _return_url="http://localhost:4200/credit-card-result";
    var _cancel_url="http://localhost:4200/credit-card-result";
    
    
      await this.service.SingleBuy(_shop_process_id, _currency, _amount??"0", _iva_amount??"0", _additional_data, _description, _return_url, _cancel_url)
      .then((res:any)=>{
        var resJson = JSON.parse(res.result)
        debugger;  
        if(resJson.status==="error") {
        
          (<HTMLInputElement>document.getElementById("resultMessage")).innerText = resJson.messages[0].dsc;
         
        }
        else{
          this.SuccessProcessId = resJson.process_id;
            
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