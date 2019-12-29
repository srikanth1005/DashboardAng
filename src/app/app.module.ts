import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';
// import {MatToolbarModule} from '@angular/material'
import { GoogleChartsModule } from 'angular-google-charts';
import {ChartsModule} from 'ng2-charts';
import { BarChartComponent } from './Components/bar-chart/bar-chart.component';
import { PieChartComponent } from './Components/pie-chart/pie-chart.component';
import { HttpClientModule } from '@angular/common/http';

import {PostelCode} from './Model/ChartDetail-Model';
import {ChartDetailService} from './Service/ChartDetail-Service';
import { HeaderComponent } from './Components/header/header.component';
import { FooterComponent } from './Components/footer/footer.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    BarChartComponent,
    PieChartComponent,
    HeaderComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,GoogleChartsModule,ChartsModule,HttpClientModule
  ],
  providers: [ChartDetailService],
  bootstrap: [AppComponent]
})
export class AppModule { }
