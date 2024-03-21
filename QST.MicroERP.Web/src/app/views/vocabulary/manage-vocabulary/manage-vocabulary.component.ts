import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { VocabDetailComponent } from '../vocab-detail/vocab-detail.component';
import { VocabularyService } from '../vocabulary.service';
import { Component, OnInit } from '@angular/core';
import { VocabularyVM } from '../Models/VocabularyVM';
import { UserVocabularyVM } from '../Models/UserVocabularyVM';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import Swal from 'sweetalert2';
import { AppConstants } from 'src/app/app.constants';
import { StorageService } from 'src/app/storage.service';
import { MatTableDataSource } from '@angular/material/table';
import { RouteIds } from '../../catalog/Models/Enums/RouteIds';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { EnumTypes } from 'src/app/enums/enumTypes';

@Component({
  selector: 'app-manage-vocabulary',
  templateUrl: './manage-vocabulary.component.html',
  styleUrls: ['./manage-vocabulary.component.css']
})
export class ManageVocabularyComponent implements OnInit {
  vocabColumns: string[] = ['word', 'englishmeaning', 'urduMeaning', 'sentence', 'pronunciation', 'novel', 'chapter', 'difficultyLevel', 'actions'];
  AddMode: boolean = true
  EditMode: boolean = false
  Add: boolean = true;
  dataSource: any;
  dialogRef: any;
  detailDialogRef: any;
  selectedVocabulary: VocabularyVM
  vocab: VocabularyVM[] = []
  userVocab: UserVocabularyVM[]
  vocLength: number
  isReadOnly: boolean = false
  novels: SettingsVM[] = []
  isLoading: boolean = false
  constructor(
    public dialog: MatDialog,
    private vocSvc: VocabularyService,
    private storeSvc: StorageService,
    private catSvc: CatalogService) {
    this.selectedVocabulary = new VocabularyVM
    this.selectedVocabulary.userVocab = new UserVocabularyVM
  }
  ngOnInit(): void {
    this.isReadOnly = this.catSvc.getPermission(RouteIds.ManageVocabulary)
    this.GetVocabulary();
    this.GetNovels()
  }
  SearchByNovel() {
    var vocab = this.vocab.filter(x => x.userVocab?.novelId == this.selectedVocabulary.userVocab.novelId)
    this.dataSource = vocab
    this.vocLength = vocab.length
  }
  GetVocabulary() {
    this.isLoading = true
    //this.selectedVocabulary.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    //this.selectedVocabulary.includeSubordinatesData = true
    this.selectedVocabulary.userVocab.userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
    this.selectedVocabulary.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.selectedVocabulary.isActive = true
    this.vocSvc.SearchVocabulary(this.selectedVocabulary).subscribe({
      next: (vocabulary: VocabularyVM[]) => {
        this.isLoading = false
        this.vocab = vocabulary;
        this.dataSource = this.vocab;
        this.vocLength = this.vocab.length
      },
      error: (err) => {
        this.isLoading = false
        this.catSvc.ErrorMsgBar();
      },
    });
  }
  GetNovels() {
    var Settings = new SettingsVM;
    Settings.enumTypeId = EnumTypes.Novels;
    Settings.isActive = true
    Settings.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.catSvc.SearchSettings(Settings).subscribe({
      next: (res) => {
        this.novels = res
      }, error: () => {
        this.catSvc.ErrorMsgBar()
      }
    })
  }
  Refresh() {
    this.GetVocabulary();
    this.selectedVocabulary = new VocabularyVM
    this.selectedVocabulary.userVocab = new UserVocabularyVM
  }
  DeleteVocabulary(id) {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        this.vocSvc.DeleteVocabulary(id).subscribe({
          next: (result) => {
            result.resultMessages.forEach(element => {
              if (element.messageType != AppConstants.ERROR_MESSAGE_TYPE) {
                this.catSvc.SuccessMsgBar(element.message)
              }
              else
                this.catSvc.ErrorMsgBar(element.message)
            })
            this.ngOnInit()
          }, error: (e) => {
            this.catSvc.ErrorMsgBar()
            console.warn(e);
          }
        })
      }
    })
  }
  OpenDialog() {
    debugger
    this.dialogRef = this.dialog.open(VocabDetailComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1200px', height: '650px'
      , data: {}
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  GetvocabForEdit(row) {
    this.dialogRef = this.dialog.open(VocabDetailComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '1200px', height: '650px'
      , data: { vocab: row }
    });
    this.dialogRef.afterClosed()
      .subscribe((res) => {
        this.ngOnInit()
      });
  }
  updateFilter(event) {
    if (this.vocab.length > 0) {
      const val = event.target.value.toLowerCase();
      var columns = Object.keys(this.vocab[0]);
      columns.splice(columns.length - 1);
      if (!columns.length)
        return;
      const rows = this.vocab.filter(function (d) {
        for (let i = 0; i <= columns.length; i++) {
          let column = columns[i];
          if (d[column] && d[column].toString().toLowerCase().indexOf(val) > -1) {
            return true;
          }
        }
        return false;
      });
      this.dataSource = new MatTableDataSource(rows);
      this.vocLength = this.dataSource.filteredData.length;
    }
  }
}

