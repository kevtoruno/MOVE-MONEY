/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { OrderResumeComponent } from './order-resume.component';

describe('OrderResumeComponent', () => {
  let component: OrderResumeComponent;
  let fixture: ComponentFixture<OrderResumeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderResumeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderResumeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
