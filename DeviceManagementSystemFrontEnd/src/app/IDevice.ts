import { IUser } from "./IUser";

export interface IDevice{
    Id?: number;
    Name: string;
    Manufacturer: string;
    Type: string;
    OS: string;
    OSVersion: string;
    Processor: string;
    RAM: number;
    Id_User?: number;
    User?: IUser;
}