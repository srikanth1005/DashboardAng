import { Component, OnInit } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { PostelCode } from '../Model/ChartDetail-Model';
import { ChartDetailService } from '../Service/ChartDetail-Service';
import { interval, Subscription } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  public projects:string[] = new Array("Mary","Tom","Jack","Jill");
  public ChartServiceResponse: PostelCode;
  statusText: string;
  subscription: Subscription;
  public cnt:number = 0;
  public totalProj:number = this.projects.length;

  public barChartData: ChartDataSets[] = [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A' },
    { data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B' },
    { data: [28, 48, 40, 19, 86, 27, 90], label: 'Series C' },
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series D' },
    { data: [28, 48, 40, 19, 86, 27, 90], label: 'Series E' },
  ];
  
  constructor(public chartservice: ChartDetailService) { }

  ngOnInit() {
    this.LoadData();
    const source = interval(10000);
  
    this.subscription = source.subscribe(val => {
      if(this.totalProj == this.cnt)
      {
        this.cnt = 0;
      }
      this.LoadData();
    });

  }

  LoadData() {
    console.log(this.projects[this.cnt]+'************');
    this.chartservice.getPostelCode(612803).subscribe((data: {}) => {
      console.log(data);
      this.cnt = this.cnt+1;
    })
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
