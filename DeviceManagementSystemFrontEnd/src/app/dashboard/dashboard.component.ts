import { Component, OnInit } from '@angular/core';
import { IDevice } from '../IDevice';
import { WebApiHandlerService } from '../web-api-handler.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
})
export class DashboardComponent implements OnInit {

  constructor(private webAPI: WebApiHandlerService) {
    this.add = false;
    this.edit = false;
    this.view = false;
    this.devices = [];
    this.devicesUsers = []
    this.deviceToEdit = '';
    this.currentDevice = {Name:"ss",Manufacturer:"cs",Type:"s",OS:"dsa",OSVersion:"SD",Processor:"sa",RAM:1};
    this.deviceToViewId = -1;
   }
  public edit: boolean;
  public add: boolean;
  public view: boolean;
  public devices: IDevice[];
  public devicesUsers: string[];
  public deviceToEdit: string;
  public currentDevice: IDevice;
  public deviceToViewId: number;
  ngOnInit(): void {
    this.webAPI.getDevices().subscribe(dbDevices => this.devices = dbDevices);
  }

  addClick(){
    this.add = true;
    this.edit = false;
    this.view = false;
  }
  editClick(id: number){
    this.edit = true;
    this.add = false;
    this.view = false;
    this.currentDevice = this.devices[id];
  }
  async deleteClick(id: number){
    this.webAPI.deleteDevice(this.devices[id].Id);
    await new Promise(resolve => setTimeout(resolve, 500));
    window.location.reload();
  }
  detailsClick(id: number | undefined){
    this.view = true;
    this.edit = false;
    this.add = false;
    if(id != undefined){
      this.deviceToViewId = id;
    }
    console.log(this.deviceToViewId);
  }
}
