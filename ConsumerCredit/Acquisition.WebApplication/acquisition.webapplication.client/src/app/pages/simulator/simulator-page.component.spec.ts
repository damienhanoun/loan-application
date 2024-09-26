import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SimulatorPageComponent } from './simulator-page.component';

describe('SimulatorComponent', () => {
  let component: SimulatorPageComponent;
  let fixture: ComponentFixture<SimulatorPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SimulatorPageComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(SimulatorPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
