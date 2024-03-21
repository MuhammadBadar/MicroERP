import { Component, } from '@angular/core';
import { VocabularyVM } from '../../vocabulary/Models/VocabularyVM';
import { UserVocabularyVM } from '../../vocabulary/Models/UserVocabularyVM'
import { CatalogService } from 'src/app/views/catalog/catalog.service';
import { NgForm } from '@angular/forms';
import { Injector, OnInit, ViewChild } from '@angular/core';
import { SettingsVM } from '../../catalog/Models/SettingsVM';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AppConstants } from 'src/app/app.constants';
import { ManageEnumLineComponent } from '../../catalog/manage-enum-line/manage-enum-line.component';
import { EnumTypes } from '../../../enums/enumTypes';
import { ManageEnumLineWithParentComponent } from '../../catalog/manage-enum-line-with-parent/manage-enum-line-with-parent.component';
import Swal from 'sweetalert2';
import { StorageService } from 'src/app/storage.service';
import { VocabularyService } from '../vocabulary.service';
@Component({
  selector: 'app-vocab-detail',
  templateUrl: './vocab-detail.component.html',
  styleUrls: ['./vocab-detail.component.css']
})
export class VocabDetailComponent implements OnInit {
  AddMode: boolean = true
  EditMode: boolean = false
  Add: boolean = true;
  Edit: boolean = true;
  dialogRef: any
  vocabDialogRef: any
  dialogData: any
  selectedVocabulary: VocabularyVM
  selectedUserVocabulary: UserVocabularyVM
  difficultyLevel: SettingsVM[];
  novels: SettingsVM[];
  chapters: SettingsVM[];
  EnumTypes: EnumTypes[];
  @ViewChild('vocabForm', { static: true }) vocabForm!: NgForm;
  constructor(

    public dialog: MatDialog,
    private injector: Injector,
    private storeSvc: StorageService,
    private vocSvc: VocabularyService,
    private catSvc: CatalogService) {
    this.vocabDialogRef = this.injector.get(MatDialogRef, null);
    this.dialogData = this.injector.get(MAT_DIALOG_DATA, null);
    this.selectedVocabulary = new VocabularyVM
    this.selectedUserVocabulary = new UserVocabularyVM

  }
  ngOnInit(): void {
    this.GetSettings(EnumTypes.VocabDifficuiltyLevels);
    this.GetSettings(EnumTypes.Novels);
    this.GetSettings(EnumTypes.Chapters);
    this.Add = true;
    this.Edit = false;
    if (this.dialogData)
      if (this.dialogData.vocab != undefined) {
        this.selectedVocabulary = this.dialogData.vocab
        if (this.selectedVocabulary.userVocab != null)
          this.selectedUserVocabulary = this.selectedVocabulary.userVocab
        this.Edit = true
        this.Add = false
        this.SearchChapters()
      }
  }
  SaveVocabulary() {
    if (!this.vocabForm.invalid) {
      const userId = this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_USER_ID)
      this.selectedUserVocabulary.userId = userId;
      this.selectedVocabulary.userVocab = this.selectedUserVocabulary
      this.selectedVocabulary.isActive = true
      this.selectedUserVocabulary.isActive = true
      this.selectedVocabulary.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      if (this.Edit)
        this.UpdateVocabulary()
      else
        this.vocSvc.SaveVocabulary(this.selectedVocabulary).subscribe({
          next: (result) => {
            result.resultMessages.forEach(element => {
              if (element.messageType != AppConstants.ERROR_MESSAGE_TYPE) {
                this.catSvc.SuccessMsgBar(element.message)
                this.Refresh();
              }
              else
                this.catSvc.ErrorMsgBar(element.message)
            });
          }, error: (err) => {
            console.warn(err)
            this.catSvc.ErrorMsgBar()
          },
        });
    } else
      this.catSvc.ErrorMsgBar("Please fill all required Fields")
  }
  UpdateVocabulary() {
    this.vocSvc.UpdateVocabulary(this.selectedVocabulary).subscribe({
      next: (result) => {
        result.resultMessages.forEach(element => {
          if (element.messageType != AppConstants.ERROR_MESSAGE_TYPE) {
            this.catSvc.SuccessMsgBar(element.message)
          }
          else
            this.catSvc.ErrorMsgBar(element.message)
        });
      }, error: (err) => {
        this.catSvc.ErrorMsgBar()
      },
    })
  }
  closeDialog(): void {
    this.dialogRef.close();
  }
  Refresh() {
    this.Add = true;
    this.Edit = false;
    var novelId = this.selectedUserVocabulary.novelId
    var chapId = this.selectedUserVocabulary.chapterId
    this.selectedUserVocabulary = new UserVocabularyVM
    this.selectedVocabulary = new VocabularyVM
    this.selectedUserVocabulary.chapterId = chapId
    this.selectedUserVocabulary.novelId = novelId
    this.SearchChapters()
  }
  GetSettings(etype: EnumTypes) {
    var Settings = new SettingsVM;
    Settings.enumTypeId = etype;
    Settings.isActive = true
    if (etype != EnumTypes.VocabDifficuiltyLevels)
      Settings.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    this.catSvc.SearchSettings(Settings).subscribe((res: SettingsVM[]) => {
      if (etype == EnumTypes.VocabDifficuiltyLevels)
        this.difficultyLevel = res;
      else if (etype == EnumTypes.Novels)
        this.novels = res;
      else if (etype == EnumTypes.Chapters)
        this.chapters = res;
    },
      (err: any) => {
        this.catSvc.ErrorMsgBar()
      });
  }
  OpenNovelDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '750px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.Novels,
        isDialog: true,
        clientId: +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.GetSettings(EnumTypes.Novels)
      }
    })
  }
  OpenChapterDialog() {
    let dialogRef = this.dialog.open(ManageEnumLineWithParentComponent, {
      disableClose: true, panelClass: 'calendar-form-dialog', width: '950px', height: '500px'
      , data: {
        enumTypeId: EnumTypes.Chapters,
        isDialog: true,
        parentType: EnumTypes.Novels,
        parentId: this.selectedUserVocabulary.novelId,
        clientId: +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
      }
    });
    dialogRef.afterClosed().subscribe({
      next: (res) => {
        this.SearchChapters()
      }
    })
  }
  SearchChapters() {
    var stng = new SettingsVM
    stng.enumTypeId = EnumTypes.Chapters
    stng.isActive = true
    stng.clientId = +this.storeSvc.getItem(AppConstants.LOCAL_STORAGE_CLIENT_ID)
    if (this.selectedUserVocabulary.novelId > 0)
      stng.parentId = this.selectedUserVocabulary.novelId
    this.catSvc.SearchSettings(stng).subscribe({
      next: (res: SettingsVM[]) => {
        this.chapters = res;
      }, error: (e) => {
        this.catSvc.ErrorMsgBar()
        console.warn(e);
      }
    })
  }
}

