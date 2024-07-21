import { BrandDto } from "../../dtos/brand.dto";

export class LoadBrands {
    static readonly type = '[Brand] Load Brands';
  }
  
  export class GetBrandById {
    static readonly type = '[Brand] Get Brand By Id';
    constructor(public id: string) {}
  }
  
  export class CreateBrand {
    static readonly type = '[Brand] Create Brand';
    constructor(public payload: BrandDto) {}
  }
  
  export class UpdateBrand {
    static readonly type = '[Brand] Update Brand';
    constructor(public id: string, public payload: BrandDto) {}
  }
  
  export class DeleteBrand {
    static readonly type = '[Brand] Delete Brand';
    constructor(public id: string) {}
  }
  