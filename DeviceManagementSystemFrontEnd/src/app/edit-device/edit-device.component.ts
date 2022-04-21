import { Component, OnInit, Input } from '@angular/core';
import { defaultIfEmpty } from 'rxjs';
import { IDevice } from '../IDevice';
import { IUser } from '../IUser';
import { WebApiHandlerService } from '../web-api-handler.service';

@Component({
  selector: 'app-edit-device',
  templateUrl: './edit-device.component.html',
  styleUrls: ['./edit-device.component.css']
})
export class EditDeviceComponent implements OnInit {

  constructor(private webAPI: WebApiHandlerService) {
    this.deviceToEdit = {Name:"ss",Manufacturer:"cs",Type:"s",OS:"dsa",OSVersion:"SD",Processor:"sa",RAM:1};
    this.webAPI.getUsers().subscribe(dbUsers => this.users = dbUsers);
   }
  @Input() deviceToEdit: IDevice;
  public users: IUser[] = [];
  ngOnInit(): void {
  }
  async editDeviceClick(name: string, manufacturer: string, type: string, os: string, osversion:string, processor: string, ram: string, selectedUser: string){
    if(name !='' && manufacturer != '' && type !='' && os != '' && osversion !='' && processor != '' && ram !=''){
      this.deviceToEdit.Name = name;
      this.deviceToEdit.Manufacturer = manufacturer;
      this.deviceToEdit.Type = type;
      this.deviceToEdit.OS = os;
      this.deviceToEdit.OSVersion = osversion;
      this.deviceToEdit.Processor = processor;
      this.deviceToEdit.RAM = parseInt(ram);
      if(selectedUser != 'null'){
        this.deviceToEdit.Id_User = this.users[parseInt(selectedUser)].Id;
      }
      else{
        this.deviceToEdit.Id_User = undefined;
      }
      this.webAPI.putDevice(this.deviceToEdit).subscribe();
      await new Promise(resolve => setTimeout(resolve, 500));
      window.location.reload();
      }
    else{
      alert("Please fill all the form fields!")
    }
  }
}
