import { Component, OnInit } from '@angular/core';
import { Store } from '@ngxs/store';
import { CreateBrand, DeleteBrand, GetBrandById, LoadBrands, UpdateBrand } from '../redux/actions/brand.action';
import { Observable } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { BrandDto } from '../dtos/brand.dto';
import { BrandState } from '../redux/states/brand.state';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';

@Component({
  selector: 'app-brand',
  standalone: true,
  imports: [ CommonModule, FormsModule, ButtonModule, InputTextModule, RippleModule],
  templateUrl: './brand.component.html',
  styleUrl: './brand.component.scss'
})
export class BrandComponent implements OnInit {

  brands$: Observable<BrandDto[]> = this.store.select(BrandState.brands);
  selectedBrand$: Observable<BrandDto> = this.store.select(BrandState.selectedBrand);
  brands: BrandDto[] = [];

  constructor(private store: Store) {
    this.getAllBrand();
  }

  ngOnInit() {
    this.loadBrands();
  }
  getAllBrand(){
    this.brands$.subscribe(
      res => {
        this.brands = res;
      }
    );
  }

  loadBrands() {
    this.store.dispatch(new LoadBrands());
  }

  getBrandById(id: string) {
    this.store.dispatch(new GetBrandById(id));
  }

  createBrand(brand: BrandDto) {
    this.store.dispatch(new CreateBrand(brand));
  }

  updateBrand(id: string, brand: BrandDto) {
    this.store.dispatch(new UpdateBrand(id, brand));
  }

  deleteBrand(id: string) {
    this.store.dispatch(new DeleteBrand(id));
  }
}
