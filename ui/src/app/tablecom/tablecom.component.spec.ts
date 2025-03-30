import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TablecomComponent } from './tablecom.component';

describe('TablecomComponent', () => {
  let component: TablecomComponent;
  let fixture: ComponentFixture<TablecomComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TablecomComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TablecomComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
