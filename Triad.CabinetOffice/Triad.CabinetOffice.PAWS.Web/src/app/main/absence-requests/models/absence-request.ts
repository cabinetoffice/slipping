import { IMemberOfParliament } from '../../members-of-parliament/models/member-of-parliament';

export interface IAbsenceRequest {
  ID: number;
  Govt_MP: string; 
  Reason: string;
  Details: string;
  Date_Created: Date;
  Status: number;
  From_Time: string;
  To_Time: string;
  From_Date: Date;
  To_Date: Date;
  From_Date_Time: Date;
  To_Date_Time: Date;
  Decision_Notes: string;
  Member_of_Parliament: IMemberOfParliament;
}
 
export class AbsenceRequest implements IAbsenceRequest {
  ID: number;
  Govt_MP: string; 
  Reason: string;
  Details: string;
  Date_Created: Date;
  Status: number;
  From_Time: string;
  To_Time: string;
  From_Date: Date;
  To_Date: Date;
  From_Date_Time: Date;
  To_Date_Time: Date;
  Decision_Notes: string;
  Member_of_Parliament: IMemberOfParliament;

  constructor(obj?: IAbsenceRequest) {
    if (obj) {
      Object.assign(this, obj);
    }
  }
}