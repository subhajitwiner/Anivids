import { State, Action, StateContext, Selector } from '@ngxs/store';
import { BrandDto } from '../../dtos/brand.dto'; // Assume this is your DTO model
import { Injectable } from '@angular/core';
import { BrandService } from '../../services/external/brand.service'; // Assume this service makes HTTP calls
import { tap } from 'rxjs/operators';
import { CreateBrand, DeleteBrand, GetBrandById, LoadBrands, UpdateBrand } from '../actions/brand.action';




export interface BrandStateModel {
  brands: BrandDto[];
  selectedBrand: BrandDto;
}

@State<BrandStateModel>({
  name: 'brand',
  defaults: {
    brands: [],
    selectedBrand: {} as BrandDto
  }
})
@Injectable()
export class BrandState {
  constructor(private brandService: BrandService) {}

  @Selector()
  static brands(state: BrandStateModel) {
    return state.brands;
  }

  @Selector()
  static selectedBrand(state: BrandStateModel) {
    return state.selectedBrand;
  }

  @Action(LoadBrands)
  loadBrands(ctx: StateContext<BrandStateModel>) {
    return this.brandService.getAll().pipe(
      tap((result) => {
        ctx.patchState({ brands: result });
      })
    );
  }

  @Action(GetBrandById)
  getBrandById(ctx: StateContext<BrandStateModel>, action: GetBrandById) {
    return this.brandService.getById(action.id).pipe(
      tap((result) => {
        ctx.patchState({ selectedBrand: result });
      })
    );
  }

  @Action(CreateBrand)
  createBrand(ctx: StateContext<BrandStateModel>, action: CreateBrand) {
    return this.brandService.create(action.payload).pipe(
      tap((result) => {
        const state = ctx.getState();
        ctx.patchState({
          brands: [...state.brands, result]
        });
      })
    );
  }

  @Action(UpdateBrand)
  updateBrand(ctx: StateContext<BrandStateModel>, action: UpdateBrand) {
    return this.brandService.update(action.id, action.payload).pipe(
      tap((result) => {
        const state = ctx.getState();
        const brands = [...state.brands];
        const index = brands.findIndex(item => item.id === action.id);
        brands[index] = result;
        ctx.patchState({ brands });
      })
    );
  }

  @Action(DeleteBrand)
  deleteBrand(ctx: StateContext<BrandStateModel>, action: DeleteBrand) {
    return this.brandService.delete(action.id).pipe(
      tap(() => {
        const state = ctx.getState();
        const filteredArray = state.brands.filter(item => item.id !== action.id);
        ctx.patchState({ brands: filteredArray });
      })
    );
  }
}
