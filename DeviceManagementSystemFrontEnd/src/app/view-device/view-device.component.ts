import { Component, Input, OnInit } from '@angular/core';
import { IDevice } from '../IDevice';
import { WebApiHandlerService } from '../web-api-handler.service';

@Component({
  selector: 'app-view-device',
  templateUrl: './view-device.component.html',
  styleUrls: ['./view-device.component.css']
})
export class ViewDeviceComponent implements OnInit {

  constructor(private webAPI: WebApiHandlerService) { 
    this.deviceToView = {Name:"ss",Manufacturer:"cs",Type:"s",OS:"dsa",OSVersion:"SD",Processor:"sa",RAM:1};
    this.deviceId = -1;
  }
  @Input() deviceId;
  public deviceToView: IDevice;
  ngOnInit(): void {
  }
  ngOnChanges(){
    this.webAPI.getDevice(this.deviceId).subscribe(dbDevice => {this.deviceToView = dbDevice; console.log(dbDevice)}); 
  }

}
