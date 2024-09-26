import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProjectsComponent } from './projects.component';
import { AcquisitionService } from '../../services/acquisition.service';
import { of } from 'rxjs';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { GetSimulatorInformationResponseDto } from '../../services/acquisition-http-service';

describe('ProjectsComponent', () => {
  let component: ProjectsComponent;
  let fixture: ComponentFixture<ProjectsComponent>;
  let acquisitionServiceSpy: jasmine.SpyObj<AcquisitionService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('AcquisitionService', ['getSimulatorInformation']);

    await TestBed.configureTestingModule({
      declarations: [ProjectsComponent],
      providers: [{provide: AcquisitionService, useValue: spy}],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();

    acquisitionServiceSpy = TestBed.inject(AcquisitionService) as jasmine.SpyObj<AcquisitionService>;
    acquisitionServiceSpy.getSimulatorInformation.and.returnValue(of({projects: ['Project1', 'Project2']} as GetSimulatorInformationResponseDto));

    fixture = TestBed.createComponent(ProjectsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set projects$ observable on init', (done: DoneFn) => {
    component.ngOnInit();
    component.projects$.subscribe(projects => {
      expect(projects).toEqual(['Project1', 'Project2']);
      done();
    });
  });

  it('should update selectedProject on project change', () => {
    const newProject = 'NewProject';
    component.onProjectChange(newProject);
    expect(component.selectedProject()).toBe(newProject);
  });

  it('should return true if selectedProject is valid', () => {
    component.onProjectChange('ValidProject');
    expect(component.isValid()).toBeTrue();
  });
});
