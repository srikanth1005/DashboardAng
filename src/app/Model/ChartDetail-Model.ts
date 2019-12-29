export class PostelCode {
    Message: string;
    Status: string;
    PostOffice: SubPostelCode[];
}

 class SubPostelCode {
    Name: String;
    Description: String;
    BranchType: String;
    DeliveryStatus: string;
    Circle: string;
    District: string;
    Division: string;
    Region: string;
    Block: string;
    State: string;
    Country: string;
    Pincode: string;
}