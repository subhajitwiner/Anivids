import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BrandDto } from '../../dtos/brand.dto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  private apiUrl = 'https://localhost:7107/api/'; // Update with your actual API URL

  constructor(private http: HttpClient) {}

  getAll(): Observable<BrandDto[]> {
    return this.http.get<BrandDto[]>(this.apiUrl+'Brand/GetAll/');
  }

  getById(id: string): Observable<BrandDto> {
    // const options = { params: new HttpParams().set('Id', id) }
    return this.http.get<BrandDto>(`${this.apiUrl}Brand/GetBrandById/${id}`);
  }

  create(payload: BrandDto): Observable<BrandDto> {
    return this.http.post<BrandDto>(this.apiUrl+'Brand/Create', payload);
  }

  update(id: string, payload: BrandDto): Observable<BrandDto> {
    const options = { params: new HttpParams().set('Id', id) }
    return this.http.put<BrandDto>(this.apiUrl+"Brand/Update/"+id, payload);
  }
  xyz(id:number,info:any){
    return this.http.put(this.apiUrl+id,info);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}Brand/DeletedBy/${id}`);
  }
}
