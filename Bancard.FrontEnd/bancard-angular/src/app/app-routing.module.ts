import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { CreditCardResultComponent } from './pages/credit-card-result/credit-card-result.component';

const routes: Routes = [ 
  { path: '', component: AppComponent , title:'index'},
  { path: 'credit-card-result', component: CreditCardResultComponent, title:'credit card' },
  { path: '',   redirectTo: '', pathMatch: 'full' },
];
export default routes;
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
