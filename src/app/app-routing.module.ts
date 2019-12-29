import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {DashboardComponent} from './dashboard/dashboard.component';
import {BarChartComponent} from '../app/Components/bar-chart/bar-chart.component';


const routes: Routes = [
  {path:'dashboard',component:DashboardComponent},
  {path:'BarChart',component:BarChartComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
