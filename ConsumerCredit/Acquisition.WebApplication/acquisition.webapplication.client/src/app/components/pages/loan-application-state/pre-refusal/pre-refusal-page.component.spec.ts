import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PreRefusalPageComponent } from './pre-refusal-page.component';

describe('PreRefusalComponent', () => {
  let component: PreRefusalPageComponent;
  let fixture: ComponentFixture<PreRefusalPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PreRefusalPageComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PreRefusalPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
