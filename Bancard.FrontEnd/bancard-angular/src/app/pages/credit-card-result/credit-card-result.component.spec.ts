import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreditCardResultComponent } from './credit-card-result.component';

describe('CreditCardResultComponent', () => {
  let component: CreditCardResultComponent;
  let fixture: ComponentFixture<CreditCardResultComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreditCardResultComponent]
    });
    fixture = TestBed.createComponent(CreditCardResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
