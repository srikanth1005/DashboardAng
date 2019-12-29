import { Component, OnInit,Input } from '@angular/core';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { SingleDataSet, Label, monkeyPatchChartJsLegend, monkeyPatchChartJsTooltip } from 'ng2-charts';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.css']
})
export class BarChartComponent implements OnInit {

@Input() chartData : any;

  public barChartOptions: ChartOptions = {};
  public barChartLabels: Label[];
  public barChartType: ChartType;
  public barChartType2: ChartType;
  public chartColors: Array<any>;
  public barChartData: ChartDataSets[];
  public barChartLegend: boolean;
  

  BarcharFun() {
    this.barChartOptions = {
      responsive: true,
      legend: { position: 'bottom' },
      // We use these empty structures as placeholders for dynamic theming.
      scales: {
        xAxes: [{
          gridLines: {
            display: false
          }
        }], yAxes: [{
          gridLines: {
            display: true
          }
        }]
      },
      plugins: {
        datalabels: {
          anchor: 'end',
          align: 'end',
        }
      }
    };

    this.barChartLabels = ['2006', '2007', '2008', '2009', '2010', '2011', '2012'];
    this.barChartType = 'bar';
    this.barChartType2 = 'line';
    this.barChartLegend = true;
    
    this.chartColors = [
      { // first color
        backgroundColor: '#3271a8'
      },
      { // second color
        backgroundColor: '#ca1414'
      },
      { // second color
        backgroundColor: 'green'
      },
      { // second color
        backgroundColor: 'block'
      }];

    this.barChartData = this.chartData;
  }

  constructor() { }

  ngOnInit() {
    this.BarcharFun();
  }

}
