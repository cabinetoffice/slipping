export interface IMemberOfParliament {
    ID: number;
    Title: string; 
    Forenames: string;
    Surname: string;
    From_Date: Date;
    To_Date: Date;
    Email_Address: string;
    Mobile: string;
    Government_Position: string;
    Notes: string;
    Status: string;
    Role: string;
    Constituency: number;
    Party: number;
    List_As: string;
    Member_Id: number;
    Dods_Id: number;
    Pims_Id: number;
    Full_Name: string;
  }
   
  export class MemberOfParliament implements IMemberOfParliament {
    ID: number;
    Title: string; 
    Forenames: string;
    Surname: string;
    From_Date: Date;
    To_Date: Date;
    Email_Address: string;
    Mobile: string;
    Government_Position: string;
    Notes: string;
    Status: string;
    Role: string;
    Constituency: number;
    Party: number;
    List_As: string;
    Member_Id: number;
    Dods_Id: number;
    Pims_Id: number;
    Full_Name: string;
  
    constructor(obj?: IMemberOfParliament) {
      if (obj) {
        Object.assign(this, obj);
      }
    }
  }