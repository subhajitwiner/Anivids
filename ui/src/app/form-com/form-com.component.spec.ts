import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormComComponent } from './form-com.component';

describe('FormComComponent', () => {
  let component: FormComComponent;
  let fixture: ComponentFixture<FormComComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [FormComComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormComComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
