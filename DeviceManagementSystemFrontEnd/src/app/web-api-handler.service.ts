import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { IDevice } from './IDevice';
import { IUser } from './IUser';

@Injectable({
  providedIn: 'root'
})
export class WebApiHandlerService {

  constructor(private httpClient: HttpClient) {
    this.port = 44321;
   }
  private port: number;
  getDevices(): Observable<IDevice[]>{
    return this.httpClient.get<IDevice[]>('https://localhost:' + this.port.toString() + '/api/devices');
  }
  getDevice(id: number): Observable<IDevice>{
    return this.httpClient.get<IDevice>('https://localhost:' + this.port.toString() + '/api/devices/' + id.toString());
  }
  postDevice(name: string, manufacturer: string, type: string, os: string, osversion:string, processor: string, ram: string): Observable<any>{
    let device: IDevice = {Name: name, Manufacturer: manufacturer, Type: type, OS: os, OSVersion: osversion, Processor:processor, RAM: parseInt(ram), User: undefined};
    const body=JSON.stringify(device);
    const headers = { 'Content-Type': 'application/json'} 
    return this.httpClient.post('https://localhost:' + this.port.toString() + '/api/devices', body,{'headers':headers});
  }
  deleteDevice(id: number | undefined){
    this.httpClient.delete('https://localhost:' + this.port.toString() + '/api/devices/' + id).subscribe();
  }
  putDevice(device: IDevice){
    const body=JSON.stringify(device);
    const headers = { 'Content-Type': 'application/json'} 
    return this.httpClient.put('https://localhost:' + this.port.toString() + '/api/devices/' + device.Id, body,{'headers':headers});
  }

  getUsers(): Observable<IUser[]>{
    return this.httpClient.get<IUser[]>('https://localhost:' + this.port.toString() + '/api/users');
  }
}
