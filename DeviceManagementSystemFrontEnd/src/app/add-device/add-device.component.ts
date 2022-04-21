import { Component, OnInit } from '@angular/core';
import { WebApiHandlerService } from '../web-api-handler.service';

@Component({
  selector: 'app-add-device',
  templateUrl: './add-device.component.html',
  styleUrls: ['./add-device.component.css']
})
export class AddDeviceComponent implements OnInit {

  constructor(private webAPI: WebApiHandlerService) { }

  ngOnInit(): void {
  }

  async addDeviceClick(name: string, manufacturer: string, type: string, os: string, osversion:string, processor: string, ram: string){
    if(name !='' && manufacturer != '' && type !='' && os != '' && osversion !='' && processor != '' && ram !=''){
      this.webAPI.postDevice(name,manufacturer,type,os, osversion,processor,ram).subscribe();
      await new Promise(resolve => setTimeout(resolve, 500));
      window.location.reload();
      }
    else{
      alert("Please fill all the form fields!")
    }
  }
}
